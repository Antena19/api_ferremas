using System.Collections.Generic;
using System.Threading.Tasks;
using Ferremas.Api.DTOs;

namespace Ferremas.Api.Services
{
    public interface IPagosService
    {
        Task<IEnumerable<PagoResponseDTO>> ObtenerTodosAsync();
        Task<PagoResponseDTO> ObtenerPorIdAsync(int id);
        Task<IEnumerable<PagoResponseDTO>> ObtenerPorPedidoAsync(int pedidoId);
        Task<PagoResponseDTO> CrearPagoAsync(PagoCreateDTO pagoCreateDTO);
        Task<PagoResponseDTO> ConfirmarPagoAsync(PagoConfirmacionDTO confirmacionDTO);
        Task<bool> PagoExisteAsync(int id);
    }
}