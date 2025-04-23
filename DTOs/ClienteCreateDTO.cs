using System.ComponentModel.DataAnnotations;

namespace Ferremas.Api.DTOs
{
    public class ClienteCreateDTO
    {
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

        [StringLength(20)]
        public string TipoCliente { get; set; } = "particular"; // particular, empresa, mayorista

        public bool Newsletter { get; set; } = false;
    }
}