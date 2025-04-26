using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ferremas.Api.Modelos
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } // PENDIENTE, EN_PROCESO, COMPLETADO, CANCELADO

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Total { get; set; }

        // Relación uno a muchos con PedidoItem
        public virtual ICollection<PedidoItem> Items { get; set; }
    }
}