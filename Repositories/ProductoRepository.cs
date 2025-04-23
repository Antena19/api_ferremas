using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Ferremas.Api.Modelos;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Ferremas.Api.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly string _connectionString;

        public ProductoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"
                    SELECT p.*, c.nombre as CategoriaNombre, m.nombre as MarcaNombre
                    FROM productos p
                    LEFT JOIN categorias c ON p.categoria_id = c.id
                    LEFT JOIN marcas m ON p.marca_id = m.id
                    WHERE p.activo = 1
                    ORDER BY p.nombre";

                var productos = await connection.QueryAsync<Producto>(sql);
                return productos;
            }
        }

        public async Task<Producto> ObtenerPorIdAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"
                    SELECT p.*, c.nombre as CategoriaNombre, m.nombre as MarcaNombre
                    FROM productos p
                    LEFT JOIN categorias c ON p.categoria_id = c.id
                    LEFT JOIN marcas m ON p.marca_id = m.id
                    WHERE p.id = @Id";

                var producto = await connection.QueryFirstOrDefaultAsync<Producto>(sql, new { Id = id });
                return producto;
            }
        }

        public async Task<IEnumerable<Producto>> BuscarProductosAsync(string termino, int? categoriaId, decimal? precioMin, decimal? precioMax)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var parameters = new DynamicParameters();

                var sql = @"
                    SELECT p.*, c.nombre as CategoriaNombre, m.nombre as MarcaNombre
                    FROM productos p
                    LEFT JOIN categorias c ON p.categoria_id = c.id
                    LEFT JOIN marcas m ON p.marca_id = m.id
                    WHERE p.activo = 1";

                if (!string.IsNullOrEmpty(termino))
                {
                    sql += " AND (p.nombre LIKE @Termino OR p.descripcion LIKE @Termino OR p.codigo LIKE @Termino)";
                    parameters.Add("Termino", $"%{termino}%");
                }

                if (categoriaId.HasValue)
                {
                    sql += " AND p.categoria_id = @CategoriaId";
                    parameters.Add("CategoriaId", categoriaId.Value);
                }

                if (precioMin.HasValue)
                {
                    sql += " AND p.precio >= @PrecioMin";
                    parameters.Add("PrecioMin", precioMin.Value);
                }

                if (precioMax.HasValue)
                {
                    sql += " AND p.precio <= @PrecioMax";
                    parameters.Add("PrecioMax", precioMax.Value);
                }

                sql += " ORDER BY p.nombre";

                var productos = await connection.QueryAsync<Producto>(sql, parameters);
                return productos;
            }
        }

        public async Task<int> CrearProductoAsync(Producto producto)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"
                    INSERT INTO productos (
                        codigo, 
                        nombre, 
                        descripcion, 
                        precio, 
                        categoria_id, 
                        marca_id, 
                        imagen_url, 
                        especificaciones, 
                        fecha_creacion,
                        activo
                    ) VALUES (
                        @Codigo, 
                        @Nombre, 
                        @Descripcion, 
                        @Precio, 
                        @CategoriaId, 
                        @MarcaId, 
                        @ImagenUrl, 
                        @Especificaciones, 
                        @FechaCreacion,
                        @Activo
                    );
                    SELECT LAST_INSERT_ID();";

                var id = await connection.ExecuteScalarAsync<int>(sql, new
                {
                    producto.Codigo,
                    producto.Nombre,
                    producto.Descripcion,
                    producto.Precio,
                    producto.CategoriaId,
                    producto.MarcaId,
                    producto.ImagenUrl,
                    producto.Especificaciones,
                    producto.FechaCreacion,
                    producto.Activo
                });

                return id;
            }
        }

        public async Task<bool> ActualizarProductoAsync(Producto producto)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = @"
                    UPDATE productos 
                    SET 
                        nombre = @Nombre, 
                        descripcion = @Descripcion, 
                        precio = @Precio, 
                        categoria_id = @CategoriaId, 
                        marca_id = @MarcaId, 
                        imagen_url = @ImagenUrl, 
                        especificaciones = @Especificaciones, 
                        fecha_modificacion = @FechaModificacion,
                        activo = @Activo
                    WHERE id = @Id";

                producto.FechaModificacion = DateTime.Now;

                var filasAfectadas = await connection.ExecuteAsync(sql, new
                {
                    producto.Id,
                    producto.Nombre,
                    producto.Descripcion,
                    producto.Precio,
                    producto.CategoriaId,
                    producto.MarcaId,
                    producto.ImagenUrl,
                    producto.Especificaciones,
                    producto.FechaModificacion,
                    producto.Activo
                });

                return filasAfectadas > 0;
            }
        }

        public async Task<bool> EliminarProductoAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // En lugar de eliminar físicamente, hacemos una baja lógica
                var sql = "UPDATE productos SET activo = 0, fecha_modificacion = @FechaModificacion WHERE id = @Id";

                var filasAfectadas = await connection.ExecuteAsync(sql, new
                {
                    Id = id,
                    FechaModificacion = DateTime.Now
                });

                return filasAfectadas > 0;
            }
        }

        public async Task<bool> ActualizarStockAsync(int productoId, int cantidad)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // Asumimos que hay una tabla de inventario relacionada con productos
                var sql = @"
                    UPDATE inventario 
                    SET stock = stock + @Cantidad,
                        ultima_salida = CASE WHEN @Cantidad < 0 THEN NOW() ELSE ultima_salida END,
                        ultimo_ingreso = CASE WHEN @Cantidad > 0 THEN NOW() ELSE ultimo_ingreso END
                    WHERE producto_id = @ProductoId";

                var filasAfectadas = await connection.ExecuteAsync(sql, new
                {
                    ProductoId = productoId,
                    Cantidad = cantidad
                });

                return filasAfectadas > 0;
            }
        }

        public async Task<bool> ProductoExisteAsync(string codigo)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = "SELECT COUNT(1) FROM productos WHERE codigo = @Codigo";

                var existe = await connection.ExecuteScalarAsync<int>(sql, new { Codigo = codigo });

                return existe > 0;
            }
        }

        public async Task<bool> ProductoExisteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var sql = "SELECT COUNT(1) FROM productos WHERE id = @Id";

                var existe = await connection.ExecuteScalarAsync<int>(sql, new { Id = id });

                return existe > 0;
            }
        }
    }
}