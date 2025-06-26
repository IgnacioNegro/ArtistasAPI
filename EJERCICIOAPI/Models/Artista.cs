using System.ComponentModel.DataAnnotations;

namespace EJERCICIOAPI.Models
{
    public class Artista
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        public string? Genero { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public string? Nacionalidad { get; set; }
        public int? CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        public Artista() { }
        public Artista(string nombre, string genero, DateOnly fechaNacimiento, string nacionalidad, int categoriaId)
        {
            Nombre = nombre;
            Genero = genero;
            FechaNacimiento = fechaNacimiento;
            Nacionalidad = nacionalidad;
            CategoriaId = categoriaId;
        }
    }
}
