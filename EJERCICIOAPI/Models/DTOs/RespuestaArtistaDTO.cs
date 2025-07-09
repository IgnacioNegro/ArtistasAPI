namespace EJEMPLOAPI.Models.DTOs
{
    public class RespuestaArtistaDTO
    {
        public int Id { get; set; } = 0;
        public string Nombre { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public string Nacionalidad { get; set; } = string.Empty;
        public string FechaNacimiento { get; set; } = string.Empty;
        public string CategoriaNombre { get; set; } = string.Empty;
        public int CategoriaId { get; set; } = 0;
        public string UsuarioEmail { get; set; } = string.Empty;
        public int UsuarioId { get; set; } = 0;
    }
}
