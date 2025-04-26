using System;
using System.ComponentModel.DataAnnotations;

namespace Ferremas.Api.DTOs
{
    public class PagoCreateDTO
    {
        [Required(ErrorMessage = "El ID del pedido es obligatorio")]
        public int PedidoId { get; set; }

        [Required(ErrorMessage = "El método de pago es obligatorio")]
        [RegularExpression("WEBPAY|TRANSFERENCIA|EFECTIVO",
            ErrorMessage = "El método de pago debe ser: WEBPAY, TRANSFERENCIA o EFECTIVO")]
        public string MetodoPago { get; set; }

        public string UrlRetorno { get; set; }  // URL para retornar después del pago (para pasarelas online)
    }

    public class PagoResponseDTO
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public string Estado { get; set; }
        public string MetodoPago { get; set; }
        public string TransaccionId { get; set; }

        // Datos necesarios para redirección a pasarela
        public string UrlPasarela { get; set; }
        public string TokenPasarela { get; set; }
    }

    public class PagoConfirmacionDTO
    {
        public string TokenPasarela { get; set; }
        public string Estado { get; set; }  // COMPLETADO, RECHAZADO
        public string TransaccionId { get; set; }
        public string DatosRespuesta { get; set; }  // JSON con datos adicionales
    }
}