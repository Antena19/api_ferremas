using System.ComponentModel.DataAnnotations;

namespace Ferremas.Api.DTOs
{
    public class DireccionCreateDTO
    {
        [Required]
        [StringLength(150)]
        public string Calle { get; set; }

        [Required]
        [StringLength(20)]
        public string Numero { get; set; }

        [StringLength(50)]
        public string Departamento { get; set; }

        [Required]
        [StringLength(100)]
        public string Comuna { get; set; }

        [Required]
        [StringLength(100)]
        public string Region { get; set; }

        [StringLength(20)]
        public string CodigoPostal { get; set; }

        public bool EsPrincipal { get; set; } = false;
    }
}