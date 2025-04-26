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

        public PagosService(IPagoRepository pagoRepository, IPedidoRepository pedidoRepository)
        {
            _pagoRepository = pagoRepository;
            _pedidoRepository = pedidoRepository;
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

            // Verificar que el pedido no haya sido pagado ya
            var pagosPedido = await _pagoRepository.ObtenerPorPedidoAsync(pagoCreateDTO.PedidoId);
            if (pagosPedido.Any(p => p.Estado == "COMPLETADO"))
                throw new InvalidOperationException("El pedido ya ha sido pagado");

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

            // Si el método es online (como WebPay), generar token
            string urlPasarela = null;
            if (pagoCreateDTO.MetodoPago == "WEBPAY")
            {
                // Aquí se integraría con la pasarela de pago
                // Por ahora, simularemos un token y URL
                nuevoPago.TokenPasarela = GenerarTokenPasarela();
                urlPasarela = $"https://webpay.example.com/pay?token={nuevoPago.TokenPasarela}";
            }

            // Guardar el pago
            var pagoId = await _pagoRepository.CrearPagoAsync(nuevoPago);
            nuevoPago.Id = pagoId;

            // Si es pago en efectivo o transferencia, marcar como pendiente de confirmación
            if (pagoCreateDTO.MetodoPago == "EFECTIVO" || pagoCreateDTO.MetodoPago == "TRANSFERENCIA")
            {
                // En estos casos, el pago queda pendiente de una confirmación manual
                await _pedidoRepository.UpdatePedidoEstadoAsync(pedido.Id, "PENDIENTE_PAGO");
            }

            // Retornar respuesta
            var respuesta = MapPagoToDTO(nuevoPago);
            respuesta.UrlPasarela = urlPasarela;

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