using System.Collections.Generic;
using System.Threading.Tasks;
using Ferremas.Api.DTOs;
using Ferremas.Api.Modelos;

namespace Ferremas.Api.Services
{
    public interface IPedidosService
    {
        Task<IEnumerable<PedidoDTO>> GetAllPedidosAsync();
        Task<PedidoDTO> GetPedidoByIdAsync(int id);
        Task<IEnumerable<PedidoDTO>> GetPedidosByClienteIdAsync(int clienteId);
        Task<PedidoDTO> CreatePedidoAsync(PedidoCreateDTO pedidoCreateDTO);
        Task<PedidoDTO> UpdatePedidoAsync(int id, PedidoUpdateDTO pedidoUpdateDTO);
        Task<PedidoDTO> UpdatePedidoEstadoAsync(int id, string estado);
        Task<bool> DeletePedidoAsync(int id);
    }
}