using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ferremas.Api.DTOs;
using Ferremas.Api.Modelos;
using Ferremas.Api.Repositories;

namespace Ferremas.Api.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IEnumerable<ClienteDTO>> ObtenerTodosAsync()
        {
            var clientes = await _clienteRepository.ObtenerTodosAsync();
            return clientes.Select(c => MapearAClienteDTO(c));
        }

        public async Task<ClienteDTO> ObtenerPorIdAsync(int id)
        {
            var cliente = await _clienteRepository.ObtenerPorIdAsync(id);
            if (cliente == null) return null;

            return MapearAClienteDTO(cliente);
        }

        public async Task<ClienteDTO> ObtenerPorRutAsync(string rut)
        {
            var cliente = await _clienteRepository.ObtenerPorRutAsync(rut);
            if (cliente == null) return null;

            return MapearAClienteDTO(cliente);
        }

        public async Task<ClienteDTO> ObtenerPorCorreoAsync(string correo)
        {
            var cliente = await _clienteRepository.ObtenerPorCorreoAsync(correo);
            if (cliente == null) return null;

            return MapearAClienteDTO(cliente);
        }

        public async Task<int> CrearClienteAsync(ClienteCreateDTO clienteDTO)
        {
            // Validar que el RUT no exista
            if (await _clienteRepository.ClienteExisteAsync(clienteDTO.Rut))
            {
                throw new InvalidOperationException($"Ya existe un cliente con el RUT {clienteDTO.Rut}");
            }

            // Validar que el correo no exista
            if (await _clienteRepository.CorreoExisteAsync(clienteDTO.CorreoElectronico))
            {
                throw new InvalidOperationException($"Ya existe un cliente con el correo {clienteDTO.CorreoElectronico}");
            }

            var cliente = new Cliente
            {
                Nombre = clienteDTO.Nombre,
                Apellido = clienteDTO.Apellido,
                Rut = clienteDTO.Rut,
                CorreoElectronico = clienteDTO.CorreoElectronico,
                Telefono = clienteDTO.Telefono,
                FechaRegistro = DateTime.Now,
                TipoCliente = clienteDTO.TipoCliente,
                Estado = "activo",
                Newsletter = clienteDTO.Newsletter
            };

            return await _clienteRepository.CrearClienteAsync(cliente);
        }

        public async Task<bool> ActualizarClienteAsync(int id, ClienteUpdateDTO clienteDTO)
        {
            var clienteExistente = await _clienteRepository.ObtenerPorIdAsync(id);
            if (clienteExistente == null) return false;

            // Actualizar propiedades
            clienteExistente.Nombre = clienteDTO.Nombre;
            clienteExistente.Apellido = clienteDTO.Apellido;
            clienteExistente.Telefono = clienteDTO.Telefono;
            clienteExistente.TipoCliente = clienteDTO.TipoCliente;
            clienteExistente.Estado = clienteDTO.Estado;
            clienteExistente.Newsletter = clienteDTO.Newsletter;

            return await _clienteRepository.ActualizarClienteAsync(clienteExistente);
        }

        public async Task<bool> EliminarClienteAsync(int id)
        {
            if (!await _clienteRepository.ClienteExisteAsync(id))
            {
                return false;
            }

            return await _clienteRepository.EliminarClienteAsync(id);
        }

        public async Task<IEnumerable<DireccionDTO>> ObtenerDireccionesAsync(int clienteId)
        {
            // Verificar si el cliente existe
            if (!await _clienteRepository.ClienteExisteAsync(clienteId))
            {
                throw new InvalidOperationException($"No existe un cliente con el ID {clienteId}");
            }

            var direcciones = await _clienteRepository.ObtenerDireccionesAsync(clienteId);
            return direcciones.Select(d => MapearADireccionDTO(d));
        }

        public async Task<DireccionDTO> ObtenerDireccionPorIdAsync(int id)
        {
            var direccion = await _clienteRepository.ObtenerDireccionPorIdAsync(id);
            if (direccion == null) return null;

            return MapearADireccionDTO(direccion);
        }

        public async Task<int> CrearDireccionAsync(int clienteId, DireccionCreateDTO direccionDTO)
        {
            // Verificar si el cliente existe
            if (!await _clienteRepository.ClienteExisteAsync(clienteId))
            {
                throw new InvalidOperationException($"No existe un cliente con el ID {clienteId}");
            }

            var direccion = new Direccion
            {
                UsuarioId = clienteId,
                Calle = direccionDTO.Calle,
                Numero = direccionDTO.Numero,
                Departamento = direccionDTO.Departamento,
                Comuna = direccionDTO.Comuna,
                Region = direccionDTO.Region,
                CodigoPostal = direccionDTO.CodigoPostal,
                EsPrincipal = direccionDTO.EsPrincipal
            };

            return await _clienteRepository.CrearDireccionAsync(direccion);
        }

        public async Task<bool> ActualizarDireccionAsync(int id, DireccionCreateDTO direccionDTO)
        {
            var direccionExistente = await _clienteRepository.ObtenerDireccionPorIdAsync(id);
            if (direccionExistente == null) return false;

            // Actualizar propiedades
            direccionExistente.Calle = direccionDTO.Calle;
            direccionExistente.Numero = direccionDTO.Numero;
            direccionExistente.Departamento = direccionDTO.Departamento;
            direccionExistente.Comuna = direccionDTO.Comuna;
            direccionExistente.Region = direccionDTO.Region;
            direccionExistente.CodigoPostal = direccionDTO.CodigoPostal;
            direccionExistente.EsPrincipal = direccionDTO.EsPrincipal;

            return await _clienteRepository.ActualizarDireccionAsync(direccionExistente);
        }

        public async Task<bool> EliminarDireccionAsync(int id)
        {
            if (!await _clienteRepository.DireccionExisteAsync(id))
            {
                return false;
            }

            return await _clienteRepository.EliminarDireccionAsync(id);
        }

        // Métodos privados para mapeo
        private ClienteDTO MapearAClienteDTO(Cliente cliente)
        {
            if (cliente == null) return null;

            return new ClienteDTO
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Rut = cliente.Rut,
                CorreoElectronico = cliente.CorreoElectronico,
                Telefono = cliente.Telefono,
                FechaRegistro = cliente.FechaRegistro,
                TipoCliente = cliente.TipoCliente,
                Estado = cliente.Estado,
                Newsletter = cliente.Newsletter,
                UltimaCompra = cliente.UltimaCompra,
                TotalCompras = cliente.TotalCompras,
                NumeroCompras = cliente.NumeroCompras,
                Direcciones = cliente.Direcciones?.Select(d => MapearADireccionDTO(d))
            };
        }

        private DireccionDTO MapearADireccionDTO(Direccion direccion)
        {
            if (direccion == null) return null;

            return new DireccionDTO
            {
                Id = direccion.Id,
                UsuarioId = direccion.UsuarioId,
                Calle = direccion.Calle,
                Numero = direccion.Numero,
                Departamento = direccion.Departamento,
                Comuna = direccion.Comuna,
                Region = direccion.Region,
                CodigoPostal = direccion.CodigoPostal,
                EsPrincipal = direccion.EsPrincipal
            };
        }
    }
}