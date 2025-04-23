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
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: api/Clientes
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClienteDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObtenerTodos()
        {
            var clientes = await _clienteService.ObtenerTodosAsync();
            return Ok(clientes);
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClienteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var cliente = await _clienteService.ObtenerPorIdAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // GET: api/Clientes/rut/12345678-9
        [HttpGet("rut/{rut}")]
        [ProducesResponseType(typeof(ClienteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObtenerPorRut(string rut)
        {
            var cliente = await _clienteService.ObtenerPorRutAsync(rut);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // GET: api/Clientes/correo/cliente@example.com
        [HttpGet("correo/{correo}")]
        [ProducesResponseType(typeof(ClienteDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObtenerPorCorreo(string correo)
        {
            var cliente = await _clienteService.ObtenerPorCorreoAsync(correo);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // POST: api/Clientes
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CrearCliente([FromBody] ClienteCreateDTO clienteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var id = await _clienteService.CrearClienteAsync(clienteDTO);
                return CreatedAtAction(nameof(ObtenerPorId), new { id }, id);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el cliente");
            }
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizarCliente(int id, [FromBody] ClienteUpdateDTO clienteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _clienteService.ActualizarClienteAsync(id, clienteDTO);

            if (!resultado)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            var resultado = await _clienteService.EliminarClienteAsync(id);

            if (!resultado)
            {
                return NotFound();
            }

            return NoContent();
        }

        // GET: api/Clientes/5/direcciones
        [HttpGet("{clienteId}/direcciones")]
        [ProducesResponseType(typeof(IEnumerable<DireccionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObtenerDirecciones(int clienteId)
        {
            try
            {
                var direcciones = await _clienteService.ObtenerDireccionesAsync(clienteId);
                return Ok(direcciones);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/Clientes/direcciones/5
        [HttpGet("direcciones/{id}")]
        [ProducesResponseType(typeof(DireccionDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObtenerDireccionPorId(int id)
        {
            var direccion = await _clienteService.ObtenerDireccionPorIdAsync(id);

            if (direccion == null)
            {
                return NotFound();
            }

            return Ok(direccion);
        }

        // POST: api/Clientes/5/direcciones
        [HttpPost("{clienteId}/direcciones")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CrearDireccion(int clienteId, [FromBody] DireccionCreateDTO direccionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var id = await _clienteService.CrearDireccionAsync(clienteId, direccionDTO);
                return CreatedAtAction(nameof(ObtenerDireccionPorId), new { id }, id);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear la dirección");
            }
        }

        // PUT: api/Clientes/direcciones/5
        [HttpPut("direcciones/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizarDireccion(int id, [FromBody] DireccionCreateDTO direccionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resultado = await _clienteService.ActualizarDireccionAsync(id, direccionDTO);

            if (!resultado)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Clientes/direcciones/5
        [HttpDelete("direcciones/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EliminarDireccion(int id)
        {
            var resultado = await _clienteService.EliminarDireccionAsync(id);

            if (!resultado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}