using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Ferremas.Api.Modelos;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Ferremas.Api.Repositories
{
    public class PagoRepository : IPagoRepository
    {
        private readonly string _connectionString;

        public PagoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Pago>> ObtenerTodosAsync()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"
                    SELECT p.*, ped.fecha as FechaPedido
                    FROM pagos p
                    JOIN pedidos ped ON p.pedido_id = ped.id
                    ORDER BY p.fecha_pago DESC";

                var pagos = await connection.QueryAsync<Pago>(sql);
                return pagos;
            }
        }

        public async Task<Pago> ObtenerPorIdAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"
                    SELECT p.*, ped.fecha as FechaPedido
                    FROM pagos p
                    JOIN pedidos ped ON p.pedido_id = ped.id
                    WHERE p.id = @Id";

                var pago = await connection.QueryFirstOrDefaultAsync<Pago>(sql, new { Id = id });
                return pago;
            }
        }

        public async Task<IEnumerable<Pago>> ObtenerPorPedidoAsync(int pedidoId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"
                    SELECT p.*, ped.fecha as FechaPedido
                    FROM pagos p
                    JOIN pedidos ped ON p.pedido_id = ped.id
                    WHERE p.pedido_id = @PedidoId
                    ORDER BY p.fecha_pago DESC";

                var pagos = await connection.QueryAsync<Pago>(sql, new { PedidoId = pedidoId });
                return pagos;
            }
        }

        public async Task<int> CrearPagoAsync(Pago pago)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"
                    INSERT INTO pagos (
                        pedido_id,
                        monto,
                        fecha_pago,
                        estado,
                        metodo_pago,
                        transaccion_id,
                        token_pasarela,
                        url_retorno,
                        datos_respuesta
                    ) VALUES (
                        @PedidoId,
                        @Monto,
                        @FechaPago,
                        @Estado,
                        @MetodoPago,
                        @TransaccionId,
                        @TokenPasarela,
                        @UrlRetorno,
                        @DatosRespuesta
                    );
                    SELECT LAST_INSERT_ID();";

                var id = await connection.ExecuteScalarAsync<int>(sql, new
                {
                    pago.PedidoId,
                    pago.Monto,
                    pago.FechaPago,
                    pago.Estado,
                    pago.MetodoPago,
                    pago.TransaccionId,
                    pago.TokenPasarela,
                    pago.UrlRetorno,
                    pago.DatosRespuesta
                });

                return id;
            }
        }

        public async Task<bool> ActualizarPagoAsync(Pago pago)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"
                    UPDATE pagos 
                    SET 
                        estado = @Estado,
                        metodo_pago = @MetodoPago,
                        transaccion_id = @TransaccionId,
                        token_pasarela = @TokenPasarela,
                        url_retorno = @UrlRetorno,
                        datos_respuesta = @DatosRespuesta
                    WHERE id = @Id";

                var filasAfectadas = await connection.ExecuteAsync(sql, new
                {
                    pago.Id,
                    pago.Estado,
                    pago.MetodoPago,
                    pago.TransaccionId,
                    pago.TokenPasarela,
                    pago.UrlRetorno,
                    pago.DatosRespuesta
                });

                return filasAfectadas > 0;
            }
        }

        public async Task<bool> ActualizarEstadoPagoAsync(int id, string estado, string transaccionId, string datosRespuesta)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"
                    UPDATE pagos 
                    SET 
                        estado = @Estado,
                        transaccion_id = @TransaccionId,
                        datos_respuesta = @DatosRespuesta
                    WHERE id = @Id";

                var filasAfectadas = await connection.ExecuteAsync(sql, new
                {
                    Id = id,
                    Estado = estado,
                    TransaccionId = transaccionId,
                    DatosRespuesta = datosRespuesta
                });

                return filasAfectadas > 0;
            }
        }

        public async Task<Pago> ObtenerPorTokenPasarelaAsync(string tokenPasarela)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"
                    SELECT p.*, ped.fecha as FechaPedido
                    FROM pagos p
                    JOIN pedidos ped ON p.pedido_id = ped.id
                    WHERE p.token_pasarela = @TokenPasarela";

                var pago = await connection.QueryFirstOrDefaultAsync<Pago>(sql, new { TokenPasarela = tokenPasarela });
                return pago;
            }
        }

        public async Task<bool> PagoExisteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = "SELECT COUNT(1) FROM pagos WHERE id = @Id";

                var existe = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });
                return existe > 0;
            }
        }
    }
}