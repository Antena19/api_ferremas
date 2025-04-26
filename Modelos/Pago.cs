using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ferremas.Api.Modelos
{
    public class Pago
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PedidoId { get; set; }

        [ForeignKey("PedidoId")]
        public Pedido Pedido { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Monto { get; set; }

        [Required]
        public DateTime FechaPago { get; set; }

        [Required]
        [MaxLength(50)]
        public string Estado { get; set; }  // PENDIENTE, COMPLETADO, RECHAZADO, ANULADO

        [MaxLength(50)]
        public string MetodoPago { get; set; }  // WEBPAY, TRANSFERENCIA, EFECTIVO

        [MaxLength(100)]
        public string TransaccionId { get; set; }  // ID de transacción de la pasarela de pago

        [MaxLength(250)]
        public string TokenPasarela { get; set; }  // Token utilizado con la pasarela de pago

        public string UrlRetorno { get; set; }  // URL para retornar después del pago

        public string DatosRespuesta { get; set; }  // JSON con datos adicionales de la respuesta
    }
}