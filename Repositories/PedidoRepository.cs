using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Ferremas.Api.Modelos;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Ferremas.Api.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly string _connectionString;

        public PedidoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Pedido>> GetAllPedidosAsync()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"
                    SELECT p.*, c.nombre as ClienteNombre
                    FROM pedidos p
                    JOIN clientes c ON p.cliente_id = c.id
                    ORDER BY p.fecha DESC";

                var pedidos = await connection.QueryAsync<Pedido>(sql);

                foreach (var pedido in pedidos)
                {
                    var items = await GetPedidoItemsAsync(pedido.Id);
                    pedido.Items = items as ICollection<PedidoItem>;
                }

                return pedidos;
            }
        }

        public async Task<Pedido> GetPedidoByIdAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"
                    SELECT p.*, c.nombre as ClienteNombre
                    FROM pedidos p
                    JOIN clientes c ON p.cliente_id = c.id
                    WHERE p.id = @Id";

                var pedido = await connection.QueryFirstOrDefaultAsync<Pedido>(sql, new { Id = id });

                if (pedido != null)
                {
                    var items = await GetPedidoItemsAsync(pedido.Id);
                    pedido.Items = items as ICollection<PedidoItem>;
                }

                return pedido;
            }
        }

        public async Task<IEnumerable<Pedido>> GetPedidosByClienteIdAsync(int clienteId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"
                    SELECT p.*, c.nombre as ClienteNombre
                    FROM pedidos p
                    JOIN clientes c ON p.cliente_id = c.id
                    WHERE p.cliente_id = @ClienteId
                    ORDER BY p.fecha DESC";

                var pedidos = await connection.QueryAsync<Pedido>(sql, new { ClienteId = clienteId });

                foreach (var pedido in pedidos)
                {
                    var items = await GetPedidoItemsAsync(pedido.Id);
                    pedido.Items = items as ICollection<PedidoItem>;
                }

                return pedidos;
            }
        }

        public async Task<Pedido> CreatePedidoAsync(Pedido pedido)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Iniciar transacción
                using (var transaction = await connection.BeginTransactionAsync())
                {
                    try
                    {
                        // Insertar pedido
                        var sqlPedido = @"
                            INSERT INTO pedidos (
                                cliente_id,
                                fecha,
                                estado,
                                total
                            ) VALUES (
                                @ClienteId,
                                @Fecha,
                                @Estado,
                                @Total
                            );
                            SELECT LAST_INSERT_ID();";

                        var pedidoId = await connection.ExecuteScalarAsync<int>(sqlPedido, new
                        {
                            pedido.ClienteId,
                            pedido.Fecha,
                            pedido.Estado,
                            pedido.Total
                        }, transaction);

                        pedido.Id = pedidoId;

                        // Insertar items del pedido
                        if (pedido.Items != null && pedido.Items.Count > 0)
                        {
                            var sqlItem = @"
                                INSERT INTO pedido_items (
                                    pedido_id,
                                    producto_id,
                                    cantidad,
                                    precio_unitario,
                                    subtotal
                                ) VALUES (
                                    @PedidoId,
                                    @ProductoId,
                                    @Cantidad,
                                    @PrecioUnitario,
                                    @Subtotal
                                );
                                SELECT LAST_INSERT_ID();";

                            foreach (var item in pedido.Items)
                            {
                                item.PedidoId = pedidoId;
                                var itemId = await connection.ExecuteScalarAsync<int>(sqlItem, new
                                {
                                    item.PedidoId,
                                    item.ProductoId,
                                    item.Cantidad,
                                    item.PrecioUnitario,
                                    item.Subtotal
                                }, transaction);

                                item.Id = itemId;
                            }
                        }

                        // Commit de la transacción
                        await transaction.CommitAsync();

                        // Retornar el pedido completo
                        return await GetPedidoByIdAsync(pedidoId);
                    }
                    catch (Exception)
                    {
                        // Rollback en caso de error
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }

        public async Task<Pedido> UpdatePedidoAsync(int id, Pedido pedido)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Iniciar transacción
                using (var transaction = await connection.BeginTransactionAsync())
                {
                    try
                    {
                        // Actualizar datos del pedido
                        var sqlPedido = @"
                            UPDATE pedidos 
                            SET 
                                estado = @Estado,
                                total = @Total
                            WHERE id = @Id";

                        await connection.ExecuteAsync(sqlPedido, new
                        {
                            Id = id,
                            pedido.Estado,
                            pedido.Total
                        }, transaction);

                        // Gestionar los items
                        if (pedido.Items != null && pedido.Items.Count > 0)
                        {
                            // Obtener los items actuales para ver cuáles eliminar
                            var sqlGetItems = "SELECT id FROM pedido_items WHERE pedido_id = @PedidoId";
                            var itemsActualesIds = await connection.QueryAsync<int>(sqlGetItems, new { PedidoId = id }, transaction);

                            // Crear HashSet para búsqueda rápida
                            var itemsNuevosIds = new HashSet<int>();
                            foreach (var item in pedido.Items)
                            {
                                if (item.Id > 0)
                                    itemsNuevosIds.Add(item.Id);
                            }

                            // Borrar items que ya no están en la lista
                            foreach (var itemId in itemsActualesIds)
                            {
                                if (!itemsNuevosIds.Contains(itemId))
                                {
                                    var sqlDeleteItem = "DELETE FROM pedido_items WHERE id = @Id";
                                    await connection.ExecuteAsync(sqlDeleteItem, new { Id = itemId }, transaction);
                                }
                            }

                            // Actualizar o insertar items
                            foreach (var item in pedido.Items)
                            {
                                // Si es un item existente, actualizar
                                if (item.Id > 0)
                                {
                                    var sqlUpdateItem = @"
                                        UPDATE pedido_items 
                                        SET 
                                            producto_id = @ProductoId,
                                            cantidad = @Cantidad,
                                            precio_unitario = @PrecioUnitario,
                                            subtotal = @Subtotal
                                        WHERE id = @Id";

                                    await connection.ExecuteAsync(sqlUpdateItem, new
                                    {
                                        item.Id,
                                        item.ProductoId,
                                        item.Cantidad,
                                        item.PrecioUnitario,
                                        item.Subtotal
                                    }, transaction);
                                }
                                // Si es un nuevo item, insertar
                                else
                                {
                                    var sqlInsertItem = @"
                                        INSERT INTO pedido_items (
                                            pedido_id,
                                            producto_id,
                                            cantidad,
                                            precio_unitario,
                                            subtotal
                                        ) VALUES (
                                            @PedidoId,
                                            @ProductoId,
                                            @Cantidad,
                                            @PrecioUnitario,
                                            @Subtotal
                                        );
                                        SELECT LAST_INSERT_ID();";

                                    item.PedidoId = id;
                                    var itemId = await connection.ExecuteScalarAsync<int>(sqlInsertItem, new
                                    {
                                        item.PedidoId,
                                        item.ProductoId,
                                        item.Cantidad,
                                        item.PrecioUnitario,
                                        item.Subtotal
                                    }, transaction);

                                    item.Id = itemId;
                                }
                            }
                        }

                        // Commit de la transacción
                        await transaction.CommitAsync();

                        // Retornar el pedido actualizado
                        return await GetPedidoByIdAsync(id);
                    }
                    catch (Exception)
                    {
                        // Rollback en caso de error
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }

        public async Task<Pedido> UpdatePedidoEstadoAsync(int id, string estado)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = "UPDATE pedidos SET estado = @Estado WHERE id = @Id";

                await connection.ExecuteAsync(sql, new { Id = id, Estado = estado });

                return await GetPedidoByIdAsync(id);
            }
        }

        public async Task<bool> DeletePedidoAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Iniciar transacción
                using (var transaction = await connection.BeginTransactionAsync())
                {
                    try
                    {
                        // Eliminar primero los items del pedido
                        var sqlDeleteItems = "DELETE FROM pedido_items WHERE pedido_id = @PedidoId";
                        await connection.ExecuteAsync(sqlDeleteItems, new { PedidoId = id }, transaction);

                        // Eliminar el pedido
                        var sqlDeletePedido = "DELETE FROM pedidos WHERE id = @Id";
                        var filasAfectadas = await connection.ExecuteAsync(sqlDeletePedido, new { Id = id }, transaction);

                        // Commit de la transacción
                        await transaction.CommitAsync();

                        return filasAfectadas > 0;
                    }
                    catch (Exception)
                    {
                        // Rollback en caso de error
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
        }

        public async Task<bool> PedidoExistsAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = "SELECT COUNT(1) FROM pedidos WHERE id = @Id";

                var existe = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });

                return existe > 0;
            }
        }

        private async Task<IEnumerable<PedidoItem>> GetPedidoItemsAsync(int pedidoId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"
                    SELECT pi.*, p.nombre as ProductoNombre
                    FROM pedido_items pi
                    JOIN productos p ON pi.producto_id = p.id
                    WHERE pi.pedido_id = @PedidoId";

                var items = await connection.QueryAsync<PedidoItem>(sql, new { PedidoId = pedidoId });

                return items;
            }
        }
    }
}