using System;

namespace Ferremas.Api.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Rut { get; set; }
        public string Telefono { get; set; }
        public string Role { get; set; } // Enum: 'cliente','administrador','vendedor','bodeguero','contador'
        public DateTime Fecha_Registro { get; set; }
        public DateTime Ultimo_Acceso { get; set; }
        public bool Activo { get; set; }
    }
}