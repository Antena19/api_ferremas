using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ferremas.Api.DTOs;
using Ferremas.Api.Modelos;
using Ferremas.Api.Repositories;

namespace Ferremas.Api.Services
{
    public class PagosService : IPagosService
    {
        private readonly IPagoRepository _pagoRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly MercadoPagoService _mercadoPagoService;

        public PagosService(IPagoRepository pagoRepository, IPedidoRepository pedidoRepository, MercadoPagoService mercadoPagoService)
        {
            _pagoRepository = pagoRepository;
            _pedidoRepository = pedidoRepository;
            _mercadoPagoService = mercadoPagoService;
        }

        public async Task<IEnumerable<PagoResponseDTO>> ObtenerTodosAsync()
        {
            var pagos = await _pagoRepository.ObtenerTodosAsync();
            return pagos.Select(MapPagoToDTO);
        }

        public async Task<PagoResponseDTO> ObtenerPorIdAsync(int id)
        {
            var pago = await _pagoRepository.ObtenerPorIdAsync(id);
            if (pago == null)
                return null;

            return MapPagoToDTO(pago);
        }

        public async Task<IEnumerable<PagoResponseDTO>> ObtenerPorPedidoAsync(int pedidoId)
        {
            var pagos = await _pagoRepository.ObtenerPorPedidoAsync(pedidoId);
            return pagos.Select(MapPagoToDTO);
        }

        public async Task<PagoResponseDTO> CrearPagoAsync(PagoCreateDTO pagoCreateDTO)
        {
            // Verificar que el pedido existe
            var pedido = await _pedidoRepository.GetPedidoByIdAsync(pagoCreateDTO.PedidoId);
            if (pedido == null)
                throw new KeyNotFoundException($"No se encontró el pedido con ID {pagoCreateDTO.PedidoId}");

            // Crear el nuevo pago
            var nuevoPago = new Pago
            {
                PedidoId = pagoCreateDTO.PedidoId,
                Monto = pedido.Total,
                FechaPago = DateTime.Now,
                Estado = "PENDIENTE",
                MetodoPago = pagoCreateDTO.MetodoPago,
                UrlRetorno = pagoCreateDTO.UrlRetorno
            };

            // Guardar el pago inicialmente
            var pagoId = await _pagoRepository.CrearPagoAsync(nuevoPago);
            nuevoPago.Id = pagoId;

            // Respuesta final
            var respuesta = MapPagoToDTO(nuevoPago);

            // Si el método es Mercado Pago, generar preferencia
            if (pagoCreateDTO.MetodoPago == "MERCADOPAGO")
            {
                // Crear preferencia en Mercado Pago
                var mpResponse = await _mercadoPagoService.CrearPreferenciaPago(pedido, pagoId);

                // Actualizar el pago con los datos de Mercado Pago
                nuevoPago.TokenPasarela = mpResponse.PreferenceId;
                await _pagoRepository.ActualizarPagoAsync(nuevoPago);

                // Agregar URL de pago a la respuesta
                respuesta.UrlPasarela = mpResponse.InitPoint; // o SandboxInitPoint para testing
            }

            return respuesta;
        }

        public async Task<PagoResponseDTO> ConfirmarPagoAsync(PagoConfirmacionDTO confirmacionDTO)
        {
            // Buscar el pago por el token de pasarela
            var pago = await _pagoRepository.ObtenerPorTokenPasarelaAsync(confirmacionDTO.TokenPasarela);
            if (pago == null)
                throw new KeyNotFoundException("No se encontró un pago con el token especificado");

            // Actualizar el estado del pago
            await _pagoRepository.ActualizarEstadoPagoAsync(
                pago.Id,
                confirmacionDTO.Estado,
                confirmacionDTO.TransaccionId,
                confirmacionDTO.DatosRespuesta
            );

            // Si el pago fue exitoso, actualizar el estado del pedido
            if (confirmacionDTO.Estado == "COMPLETADO")
            {
                await _pedidoRepository.UpdatePedidoEstadoAsync(pago.PedidoId, "PAGADO");
            }
            else if (confirmacionDTO.Estado == "RECHAZADO")
            {
                // Mantener el pedido como pendiente si el pago fue rechazado
                await _pedidoRepository.UpdatePedidoEstadoAsync(pago.PedidoId, "PENDIENTE");
            }

            // Obtener el pago actualizado
            pago = await _pagoRepository.ObtenerPorIdAsync(pago.Id);

            return MapPagoToDTO(pago);
        }

        public async Task<bool> PagoExisteAsync(int id)
        {
            return await _pagoRepository.PagoExisteAsync(id);
        }

        private PagoResponseDTO MapPagoToDTO(Pago pago)
        {
            return new PagoResponseDTO
            {
                Id = pago.Id,
                PedidoId = pago.PedidoId,
                Monto = pago.Monto,
                FechaPago = pago.FechaPago,
                Estado = pago.Estado,
                MetodoPago = pago.MetodoPago,
                TransaccionId = pago.TransaccionId,
                TokenPasarela = pago.TokenPasarela
            };
        }
    

        private string GenerarTokenPasarela()
        {
            // En un entorno real, esto sería generado por la pasarela de pago
            // Para simular, generamos un token aleatorio
            return Guid.NewGuid().ToString("N");
        }


    }
}