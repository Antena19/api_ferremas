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
    public class PedidosController : ControllerBase
    {
        private readonly IPedidosService _pedidosService;

        public PedidosController(IPedidosService pedidosService)
        {
            _pedidosService = pedidosService;
        }

        /// <summary>
        /// Obtiene todos los pedidos
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>> GetPedidos()
        {
            var pedidos = await _pedidosService.GetAllPedidosAsync();
            return Ok(pedidos);
        }

        /// <summary>
        /// Obtiene un pedido por su ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PedidoDTO>> GetPedido(int id)
        {
            var pedido = await _pedidosService.GetPedidoByIdAsync(id);

            if (pedido == null)
                return NotFound($"No se encontró el pedido con ID {id}");

            return Ok(pedido);
        }

        /// <summary>
        /// Obtiene los pedidos de un cliente específico
        /// </summary>
        [HttpGet("cliente/{clienteId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>> GetPedidosByCliente(int clienteId)
        {
            var pedidos = await _pedidosService.GetPedidosByClienteIdAsync(clienteId);
            return Ok(pedidos);
        }

        /// <summary>
        /// Crea un nuevo pedido
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PedidoDTO>> CreatePedido(PedidoCreateDTO pedidoCreateDTO)
        {
            try
            {
                var nuevoPedido = await _pedidosService.CreatePedidoAsync(pedidoCreateDTO);
                return CreatedAtAction(nameof(GetPedido), new { id = nuevoPedido.Id }, nuevoPedido);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Actualiza un pedido existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PedidoDTO>> UpdatePedido(int id, PedidoUpdateDTO pedidoUpdateDTO)
        {
            try
            {
                var pedidoActualizado = await _pedidosService.UpdatePedidoAsync(id, pedidoUpdateDTO);

                if (pedidoActualizado == null)
                    return NotFound($"No se encontró el pedido con ID {id}");

                return Ok(pedidoActualizado);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Actualiza el estado de un pedido
        /// </summary>
        [HttpPatch("{id}/estado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PedidoDTO>> UpdateEstadoPedido(int id, [FromBody] string estado)
        {
            if (string.IsNullOrEmpty(estado) || !new[] { "PENDIENTE", "EN_PROCESO", "COMPLETADO", "CANCELADO" }.Contains(estado))
                return BadRequest("Estado no válido. Debe ser: PENDIENTE, EN_PROCESO, COMPLETADO o CANCELADO");

            var pedidoActualizado = await _pedidosService.UpdatePedidoEstadoAsync(id, estado);

            if (pedidoActualizado == null)
                return NotFound($"No se encontró el pedido con ID {id}");

            return Ok(pedidoActualizado);
        }

        /// <summary>
        /// Elimina un pedido
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var deleted = await _pedidosService.DeletePedidoAsync(id);

            if (!deleted)
                return NotFound($"No se encontró el pedido con ID {id}");

            return NoContent();
        }
    }
}