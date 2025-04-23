using System;
using System.Collections.Generic;

namespace Ferremas.Api.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Rut { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string TipoCliente { get; set; }
        public string Estado { get; set; }
        public bool Newsletter { get; set; }
        public DateTime? UltimaCompra { get; set; }
        public decimal TotalCompras { get; set; }
        public int NumeroCompras { get; set; }
        public IEnumerable<DireccionDTO> Direcciones { get; set; }
    }
}