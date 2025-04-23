using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Ferremas.Api.Modelos;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Ferremas.Api.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string _connectionString;

        public ClienteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Cliente>> ObtenerTodosAsync()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"
                    SELECT * FROM clientes
                    WHERE estado = 'activo'
                    ORDER BY nombre, apellido";

                var clientes = await connection.QueryAsync<Cliente>(sql);

                foreach (var cliente in clientes)
                {
                    var direcciones = await ObtenerDireccionesAsync(cliente.Id);
                    cliente.Direcciones = direcciones as ICollection<Direccion>;
                }

                return clientes;
            }
        }

        public async Task<Cliente> ObtenerPorIdAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = "SELECT * FROM clientes WHERE id = @Id";

                var cliente = await connection.QueryFirstOrDefaultAsync<Cliente>(sql, new { Id = id });

                if (cliente != null)
                {
                    var direcciones = await ObtenerDireccionesAsync(cliente.Id);
                    cliente.Direcciones = direcciones as ICollection<Direccion>;
                }

                return cliente;
            }
        }

        public async Task<Cliente> ObtenerPorRutAsync(string rut)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = "SELECT * FROM clientes WHERE rut = @Rut";

                var cliente = await connection.QueryFirstOrDefaultAsync<Cliente>(sql, new { Rut = rut });

                if (cliente != null)
                {
                    var direcciones = await ObtenerDireccionesAsync(cliente.Id);
                    cliente.Direcciones = direcciones as ICollection<Direccion>;
                }

                return cliente;
            }
        }

        public async Task<Cliente> ObtenerPorCorreoAsync(string correo)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = "SELECT * FROM clientes WHERE correo_electronico = @Correo";

                var cliente = await connection.QueryFirstOrDefaultAsync<Cliente>(sql, new { Correo = correo });

                if (cliente != null)
                {
                    var direcciones = await ObtenerDireccionesAsync(cliente.Id);
                    cliente.Direcciones = direcciones as ICollection<Direccion>;
                }

                return cliente;
            }
        }

        public async Task<int> CrearClienteAsync(Cliente cliente)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"
                    INSERT INTO clientes (
                        nombre, 
                        apellido, 
                        rut, 
                        correo_electronico, 
                        telefono, 
                        fecha_registro,
                        tipo_cliente,
                        estado,
                        newsletter
                    ) VALUES (
                        @Nombre, 
                        @Apellido, 
                        @Rut, 
                        @CorreoElectronico, 
                        @Telefono, 
                        @FechaRegistro,
                        @TipoCliente,
                        @Estado,
                        @Newsletter
                    );
                    SELECT LAST_INSERT_ID();";

                var id = await connection.ExecuteScalarAsync<int>(sql, new
                {
                    cliente.Nombre,
                    cliente.Apellido,
                    cliente.Rut,
                    cliente.CorreoElectronico,
                    cliente.Telefono,
                    cliente.FechaRegistro,
                    cliente.TipoCliente,
                    cliente.Estado,
                    cliente.Newsletter
                });

                return id;
            }
        }

        public async Task<bool> ActualizarClienteAsync(Cliente cliente)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"
                    UPDATE clientes 
                    SET 
                        nombre = @Nombre, 
                        apellido = @Apellido, 
                        telefono = @Telefono, 
                        tipo_cliente = @TipoCliente, 
                        estado = @Estado, 
                        newsletter = @Newsletter
                    WHERE id = @Id";

                var filasAfectadas = await connection.ExecuteAsync(sql, new
                {
                    cliente.Id,
                    cliente.Nombre,
                    cliente.Apellido,
                    cliente.Telefono,
                    cliente.TipoCliente,
                    cliente.Estado,
                    cliente.Newsletter
                });

                return filasAfectadas > 0;
            }
        }

        public async Task<bool> EliminarClienteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // En lugar de eliminar físicamente, hacemos una baja lógica
                var sql = "UPDATE clientes SET estado = 'inactivo' WHERE id = @Id";

                var filasAfectadas = await connection.ExecuteAsync(sql, new { Id = id });

                return filasAfectadas > 0;
            }
        }

        public async Task<bool> ClienteExisteAsync(string rut)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = "SELECT COUNT(1) FROM clientes WHERE rut = @Rut";

                var existe = await connection.ExecuteScalarAsync<int>(sql, new { Rut = rut });

                return existe > 0;
            }
        }

        public async Task<bool> ClienteExisteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = "SELECT COUNT(1) FROM clientes WHERE id = @Id";

                var existe = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });

                return existe > 0;
            }
        }

        public async Task<bool> CorreoExisteAsync(string correo)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = "SELECT COUNT(1) FROM clientes WHERE correo_electronico = @Correo";

                var existe = await connection.ExecuteScalarAsync<int>(sql, new { Correo = correo });

                return existe > 0;
            }
        }

        public async Task<IEnumerable<Direccion>> ObtenerDireccionesAsync(int clienteId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = "SELECT * FROM direcciones WHERE usuario_id = @ClienteId";

                var direcciones = await connection.QueryAsync<Direccion>(sql, new { ClienteId = clienteId });

                return direcciones;
            }
        }

        public async Task<Direccion> ObtenerDireccionPorIdAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = "SELECT * FROM direcciones WHERE id = @Id";

                var direccion = await connection.QueryFirstOrDefaultAsync<Direccion>(sql, new { Id = id });

                return direccion;
            }
        }

        public async Task<int> CrearDireccionAsync(Direccion direccion)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Si es la dirección principal, desmarcar otras direcciones
                if (direccion.EsPrincipal)
                {
                    var sqlDesmarcar = @"
                        UPDATE direcciones 
                        SET es_principal = 0 
                        WHERE usuario_id = @UsuarioId";

                    await connection.ExecuteAsync(sqlDesmarcar, new { direccion.UsuarioId });
                }

                var sql = @"
                    INSERT INTO direcciones (
                        usuario_id,
                        calle,
                        numero,
                        departamento,
                        comuna,
                        region,
                        codigo_postal,
                        es_principal
                    ) VALUES (
                        @UsuarioId,
                        @Calle,
                        @Numero,
                        @Departamento,
                        @Comuna,
                        @Region,
                        @CodigoPostal,
                        @EsPrincipal
                    );
                    SELECT LAST_INSERT_ID();";

                var id = await connection.ExecuteScalarAsync<int>(sql, new
                {
                    direccion.UsuarioId,
                    direccion.Calle,
                    direccion.Numero,
                    direccion.Departamento,
                    direccion.Comuna,
                    direccion.Region,
                    direccion.CodigoPostal,
                    direccion.EsPrincipal
                });

                return id;
            }
        }

        public async Task<bool> ActualizarDireccionAsync(Direccion direccion)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Si es la dirección principal, desmarcar otras direcciones
                if (direccion.EsPrincipal)
                {
                    var sqlDesmarcar = @"
                        UPDATE direcciones 
                        SET es_principal = 0 
                        WHERE usuario_id = @UsuarioId AND id != @Id";

                    await connection.ExecuteAsync(sqlDesmarcar, new
                    {
                        direccion.UsuarioId,
                        direccion.Id
                    });
                }

                var sql = @"
                    UPDATE direcciones 
                    SET 
                        calle = @Calle,
                        numero = @Numero,
                        departamento = @Departamento,
                        comuna = @Comuna,
                        region = @Region,
                        codigo_postal = @CodigoPostal,
                        es_principal = @EsPrincipal
                    WHERE id = @Id";

                var filasAfectadas = await connection.ExecuteAsync(sql, new
                {
                    direccion.Id,
                    direccion.Calle,
                    direccion.Numero,
                    direccion.Departamento,
                    direccion.Comuna,
                    direccion.Region,
                    direccion.CodigoPostal,
                    direccion.EsPrincipal
                });

                return filasAfectadas > 0;
            }
        }

        public async Task<bool> EliminarDireccionAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = "DELETE FROM direcciones WHERE id = @Id";

                var filasAfectadas = await connection.ExecuteAsync(sql, new { Id = id });

                return filasAfectadas > 0;
            }
        }

        public async Task<bool> DireccionExisteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = "SELECT COUNT(1) FROM direcciones WHERE id = @Id";

                var existe = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });

                return existe > 0;
            }
        }
    }
}