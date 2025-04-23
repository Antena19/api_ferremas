using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ferremas.Api.Modelos
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(12)]
        public string Rut { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string CorreoElectronico { get; set; }

        [StringLength(20)]
        public string Telefono { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [StringLength(20)]
        public string TipoCliente { get; set; } = "particular"; // particular, empresa, mayorista

        [StringLength(20)]
        public string Estado { get; set; } = "activo"; // activo, inactivo

        public bool Newsletter { get; set; } = false;

        public DateTime? UltimaCompra { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalCompras { get; set; } = 0;

        public int NumeroCompras { get; set; } = 0;

        // Propiedades de navegación
        public virtual ICollection<Direccion> Direcciones { get; set; }
    }
}