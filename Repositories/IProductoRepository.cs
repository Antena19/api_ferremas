using System.Collections.Generic;
using System.Threading.Tasks;
using Ferremas.Api.Modelos;

namespace Ferremas.Api.Repositories
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> ObtenerTodosAsync();
        Task<Producto> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Producto>> BuscarProductosAsync(string termino, int? categoriaId, decimal? precioMin, decimal? precioMax);
        Task<int> CrearProductoAsync(Producto producto);
        Task<bool> ActualizarProductoAsync(Producto producto);
        Task<bool> EliminarProductoAsync(int id);
        Task<bool> ActualizarStockAsync(int productoId, int cantidad);
        Task<bool> ProductoExisteAsync(string codigo);
        Task<bool> ProductoExisteAsync(int id);
    }
}