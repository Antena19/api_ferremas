using System.ComponentModel.DataAnnotations;

namespace Ferremas.Api.DTOs
{
    public class ProductoCreateDTO
    {
        [Required]
        [StringLength(50)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [Required]
        [Range(0.01, 9999999.99)]
        public decimal Precio { get; set; }

        public int? CategoriaId { get; set; }

        public int? MarcaId { get; set; }

        public string ImagenUrl { get; set; }

        public string Especificaciones { get; set; }
    }
}