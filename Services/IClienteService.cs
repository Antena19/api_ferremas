using System.Collections.Generic;
using System.Threading.Tasks;
using Ferremas.Api.DTOs;

namespace Ferremas.Api.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDTO>> ObtenerTodosAsync();
        Task<ClienteDTO> ObtenerPorIdAsync(int id);
        Task<ClienteDTO> ObtenerPorRutAsync(string rut);
        Task<ClienteDTO> ObtenerPorCorreoAsync(string correo);
        Task<int> CrearClienteAsync(ClienteCreateDTO clienteDTO);
        Task<bool> ActualizarClienteAsync(int id, ClienteUpdateDTO clienteDTO);
        Task<bool> EliminarClienteAsync(int id);

        // Métodos para direcciones
        Task<IEnumerable<DireccionDTO>> ObtenerDireccionesAsync(int clienteId);
        Task<DireccionDTO> ObtenerDireccionPorIdAsync(int id);
        Task<int> CrearDireccionAsync(int clienteId, DireccionCreateDTO direccionDTO);
        Task<bool> ActualizarDireccionAsync(int id, DireccionCreateDTO direccionDTO);
        Task<bool> EliminarDireccionAsync(int id);
    }
}