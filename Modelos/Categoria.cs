using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ferremas.Api.Modelos
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int? CategoriaPadreId { get; set; }

        public bool Activo { get; set; } = true;

        // Propiedad de navegación
        public virtual Categoria CategoriaPadre { get; set; }
        public virtual ICollection<Categoria> SubCategorias { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}