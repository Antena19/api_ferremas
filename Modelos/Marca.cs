using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ferremas.Api.Modelos
{
    public class Marca
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [StringLength(255)]
        public string LogoUrl { get; set; }

        public bool Activo { get; set; } = true;

        // Propiedad de navegación
        public virtual ICollection<Producto> Productos { get; set; }
    }
}