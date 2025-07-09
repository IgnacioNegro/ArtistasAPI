using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EJERCICIOAPI.Models.DTOs
{
    public class ArtistaDTO
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;
        public string? Genero { get; set; }
        [Required]
        public DateOnly FechaNacimiento { get; set; }
        public string? Nacionalidad { get; set; }
        [Required]
        public int CategoriaId { get; set; } = 0;
    }
}
