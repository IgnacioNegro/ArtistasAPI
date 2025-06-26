using System.ComponentModel.DataAnnotations;

namespace EJERCICIOAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        [Required]
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public List<Artista>? Artistas { get; set; } = new List<Artista>();
    }
}
