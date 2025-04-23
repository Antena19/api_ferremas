using System.Collections.Generic;
using System.Threading.Tasks;
using Ferremas.Api.Modelos;

namespace Ferremas.Api.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ObtenerTodosAsync();
        Task<Cliente> ObtenerPorIdAsync(int id);
        Task<Cliente> ObtenerPorRutAsync(string rut);
        Task<Cliente> ObtenerPorCorreoAsync(string correo);
        Task<int> CrearClienteAsync(Cliente cliente);
        Task<bool> ActualizarClienteAsync(Cliente cliente);
        Task<bool> EliminarClienteAsync(int id);
        Task<bool> ClienteExisteAsync(string rut);
        Task<bool> ClienteExisteAsync(int id);
        Task<bool> CorreoExisteAsync(string correo);

        // Métodos para direcciones
        Task<IEnumerable<Direccion>> ObtenerDireccionesAsync(int clienteId);
        Task<Direccion> ObtenerDireccionPorIdAsync(int id);
        Task<int> CrearDireccionAsync(Direccion direccion);
        Task<bool> ActualizarDireccionAsync(Direccion direccion);
        Task<bool> EliminarDireccionAsync(int id);
        Task<bool> DireccionExisteAsync(int id);
    }
}