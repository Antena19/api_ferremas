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
    public class PagosController : ControllerBase
    {
        private readonly IPagosService _pagosService;

        public PagosController(IPagosService pagosService)
        {
            _pagosService = pagosService;
        }



        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PagoResponseDTO>>> ObtenerPagos()
        {
            var pagos = await _pagosService.ObtenerTodosAsync();
            return Ok(pagos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PagoResponseDTO>> ObtenerPago(int id)
        {
            var pago = await _pagosService.ObtenerPorIdAsync(id);

            if (pago == null)
                return NotFound($"No se encontró el pago con ID {id}");

            return Ok(pago);
        }

        [HttpGet("pedido/{pedidoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PagoResponseDTO>>> ObtenerPagosPorPedido(int pedidoId)
        {
            var pagos = await _pagosService.ObtenerPorPedidoAsync(pedidoId);
            return Ok(pagos);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagoResponseDTO>> CrearPago(PagoCreateDTO pagoCreateDTO)
        {
            try
            {
                var nuevoPago = await _pagosService.CrearPagoAsync(pagoCreateDTO);
                return CreatedAtAction(nameof(ObtenerPago), new { id = nuevoPago.Id }, nuevoPago);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("confirmar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagoResponseDTO>> ConfirmarPago(PagoConfirmacionDTO confirmacionDTO)
        {
            try
            {
                var pagoConfirmado = await _pagosService.ConfirmarPagoAsync(confirmacionDTO);
                return Ok(pagoConfirmado);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}