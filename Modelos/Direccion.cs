using System.ComponentModel.DataAnnotations;

namespace Ferremas.Api.Modelos
{
    public class Direccion
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

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

        // Propiedad de navegación
        public virtual Cliente Usuario { get; set; }
    }
}