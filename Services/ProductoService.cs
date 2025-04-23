using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ferremas.Api.DTOs;
using Ferremas.Api.Modelos;
using Ferremas.Api.Repositories;

namespace Ferremas.Api.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<IEnumerable<ProductoDTO>> ObtenerTodosAsync()
        {
            var productos = await _productoRepository.ObtenerTodosAsync();
            return productos.Select(p => MapearADTO(p));
        }

        public async Task<ProductoDTO> ObtenerPorIdAsync(int id)
        {
            var producto = await _productoRepository.ObtenerPorIdAsync(id);
            if (producto == null) return null;

            return MapearADTO(producto);
        }

        public async Task<IEnumerable<ProductoDTO>> BuscarProductosAsync(string termino, int? categoriaId, decimal? precioMin, decimal? precioMax)
        {
            var productos = await _productoRepository.BuscarProductosAsync(termino, categoriaId, precioMin, precioMax);
            return productos.Select(p => MapearADTO(p));
        }

        public async Task<int> CrearProductoAsync(ProductoCreateDTO productoDTO)
        {
            if (await _productoRepository.ProductoExisteAsync(productoDTO.Codigo))
            {
                throw new InvalidOperationException($"Ya existe un producto con el código {productoDTO.Codigo}");
            }

            var producto = new Producto
            {
                Codigo = productoDTO.Codigo,
                Nombre = productoDTO.Nombre,
                Descripcion = productoDTO.Descripcion,
                Precio = productoDTO.Precio,
                CategoriaId = productoDTO.CategoriaId,
                MarcaId = productoDTO.MarcaId,
                ImagenUrl = productoDTO.ImagenUrl,
                Especificaciones = productoDTO.Especificaciones,
                FechaCreacion = DateTime.Now,
                Activo = true
            };

            return await _productoRepository.CrearProductoAsync(producto);
        }

        public async Task<bool> ActualizarProductoAsync(int id, ProductoUpdateDTO productoDTO)
        {
            var productoExistente = await _productoRepository.ObtenerPorIdAsync(id);
            if (productoExistente == null) return false;

            // Actualizar propiedades
            productoExistente.Nombre = productoDTO.Nombre;
            productoExistente.Descripcion = productoDTO.Descripcion;
            productoExistente.Precio = productoDTO.Precio;
            productoExistente.CategoriaId = productoDTO.CategoriaId;
            productoExistente.MarcaId = productoDTO.MarcaId;
            productoExistente.ImagenUrl = productoDTO.ImagenUrl;
            productoExistente.Especificaciones = productoDTO.Especificaciones;
            productoExistente.Activo = productoDTO.Activo;
            productoExistente.FechaModificacion = DateTime.Now;

            return await _productoRepository.ActualizarProductoAsync(productoExistente);
        }

        public async Task<bool> EliminarProductoAsync(int id)
        {
            if (!await _productoRepository.ProductoExisteAsync(id))
            {
                return false;
            }

            return await _productoRepository.EliminarProductoAsync(id);
        }

        public async Task<bool> ActualizarStockAsync(int productoId, int cantidad)
        {
            if (!await _productoRepository.ProductoExisteAsync(productoId))
            {
                return false;
            }

            return await _productoRepository.ActualizarStockAsync(productoId, cantidad);
        }

        // Método auxiliar para mapear de Modelo a DTO
        private ProductoDTO MapearADTO(Producto producto)
        {
            if (producto == null) return null;

            return new ProductoDTO
            {
                Id = producto.Id,
                Codigo = producto.Codigo,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                CategoriaId = producto.CategoriaId,
                CategoriaNombre = producto.Categoria?.Nombre,
                MarcaId = producto.MarcaId,
                MarcaNombre = producto.Marca?.Nombre,
                ImagenUrl = producto.ImagenUrl,
                Especificaciones = producto.Especificaciones,
                FechaCreacion = producto.FechaCreacion,
                Activo = producto.Activo
            };
        }
    }
}