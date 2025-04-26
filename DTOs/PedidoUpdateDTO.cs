using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ferremas.Api.DTOs
{
    public class PedidoUpdateDTO
    {
        [Required(ErrorMessage = "El estado del pedido es obligatorio")]
        [RegularExpression("PENDIENTE|EN_PROCESO|COMPLETADO|CANCELADO",
            ErrorMessage = "El estado debe ser uno de los siguientes: PENDIENTE, EN_PROCESO, COMPLETADO, CANCELADO")]
        public string Estado { get; set; }

        public List<PedidoItemUpdateDTO> Items { get; set; }
    }

    public class PedidoItemUpdateDTO
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "El ID del producto es obligatorio")]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }
    }
}