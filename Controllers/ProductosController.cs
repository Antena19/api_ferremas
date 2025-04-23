using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ferremas.Api.DTOs;
using Ferremas.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ferremas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        // GET: api/Productos
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductoDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObtenerTodos()
        {
            var productos = await _productoService.ObtenerTodosAsync();
            return Ok(productos);
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var producto = await _productoService.ObtenerPorIdAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // GET: api/Productos/buscar?termino=taladro&categoriaId=2&precioMin=1000&precioMax=5000
        [HttpGet("buscar")]
        [ProducesResponseType(typeof(IEnumerable<ProductoDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> BuscarProductos(
            [FromQuery] string termino,
            [FromQuery] int? categoriaId,
            [FromQuery] decimal? precioMin,
            [FromQuery] decimal? precioMax)
        {
            var productos = await _productoService.BuscarProductosAsync(termino, categoriaId, precioMin, precioMax);
            return Ok(productos);
        }

        // POST: api/Productos
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CrearProducto([FromBody] ProductoCreateDTO productoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var id = await _productoService.CrearProductoAsync(productoDTO);
                return CreatedAtAction(nameof(ObtenerPorId), new { id }, id);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el producto");
            }
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizarProducto(int id, [FromBody] ProductoUpdateDTO productoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _productoService.ActualizarProductoAsync(id, productoDTO);

            if (!resultado)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var resultado = await _productoService.EliminarProductoAsync(id);

            if (!resultado)
            {
                return NotFound();
            }

            return NoContent();
        }

        // PATCH: api/Productos/5/stock/10
        [HttpPatch("{id}/stock/{cantidad}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizarStock(int id, int cantidad)
        {
            var resultado = await _productoService.ActualizarStockAsync(id, cantidad);

            if (!resultado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
