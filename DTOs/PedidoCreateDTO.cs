using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ferremas.Api.DTOs
{
    public class PedidoCreateDTO
    {
        [Required(ErrorMessage = "El ID del cliente es obligatorio")]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "Los items del pedido son obligatorios")]
        public List<PedidoItemDTO> Items { get; set; }
    }

    public class PedidoItemDTO
    {
        [Required(ErrorMessage = "El ID del producto es obligatorio")]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }
    }
}