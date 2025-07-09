using System.ComponentModel.DataAnnotations;

namespace EJERCICIOAPI.Models.DTOs
{
    public class EspectaculoRecibirDTO
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public DateTime Fechayhora { get; set; }

        [Required]
        public int ArtistaId { get; set; }

        public string ArtistaNombre { get; set; } = string.Empty;
        
    }
}