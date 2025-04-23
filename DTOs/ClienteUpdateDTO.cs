using System.ComponentModel.DataAnnotations;

namespace Ferremas.Api.DTOs
{
    public class ClienteUpdateDTO
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        [StringLength(20)]
        public string Telefono { get; set; }

        public string TipoCliente { get; set; }

        public string Estado { get; set; }

        public bool Newsletter { get; set; }
    }
}