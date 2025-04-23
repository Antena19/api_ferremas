using System;

namespace Ferremas.Api.DTOs
{
    public class ProductoDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int? CategoriaId { get; set; }
        public string CategoriaNombre { get; set; }
        public int? MarcaId { get; set; }
        public string MarcaNombre { get; set; }
        public string ImagenUrl { get; set; }
        public string Especificaciones { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
    }
}