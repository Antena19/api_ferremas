using System;
using System.Collections.Generic;

namespace Ferremas.Api.DTOs
{
    public class PedidoDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; }
        public string Estado { get; set; }
        public decimal Total { get; set; }
        public List<PedidoItemResponseDTO> Items { get; set; }
    }

    public class PedidoItemResponseDTO
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}