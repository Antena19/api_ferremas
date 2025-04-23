namespace Ferremas.Api.DTOs
{
    public class DireccionDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Departamento { get; set; }
        public string Comuna { get; set; }
        public string Region { get; set; }
        public string CodigoPostal { get; set; }
        public bool EsPrincipal { get; set; }
    }
}