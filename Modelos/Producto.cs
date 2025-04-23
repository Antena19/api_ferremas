using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ferremas.Api.Modelos
{
    public class Producto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Precio { get; set; }

        public int? CategoriaId { get; set; }

        public int? MarcaId { get; set; }

        [StringLength(255)]
        public string ImagenUrl { get; set; }

        public string Especificaciones { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public DateTime? FechaModificacion { get; set; }

        public bool Activo { get; set; } = true;

        // Propiedades de navegación
        public virtual Categoria Categoria { get; set; }
        public virtual Marca Marca { get; set; }
    }
}
