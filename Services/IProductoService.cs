using System.Collections.Generic;
using System.Threading.Tasks;
using Ferremas.Api.DTOs;
using Ferremas.Api.Modelos;

namespace Ferremas.Api.Services
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDTO>> ObtenerTodosAsync();
        Task<ProductoDTO> ObtenerPorIdAsync(int id);
        Task<IEnumerable<ProductoDTO>> BuscarProductosAsync(string termino, int? categoriaId, decimal? precioMin, decimal? precioMax);
        Task<int> CrearProductoAsync(ProductoCreateDTO productoDTO);
        Task<bool> ActualizarProductoAsync(int id, ProductoUpdateDTO productoDTO);
        Task<bool> EliminarProductoAsync(int id);
        Task<bool> ActualizarStockAsync(int productoId, int cantidad);
    }
}