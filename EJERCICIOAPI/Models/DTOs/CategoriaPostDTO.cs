using System.ComponentModel.DataAnnotations;
namespace EJERCICIOAPI.Models.DTOs

{
    public class CategoriaPostDTO
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;
              
    }
}
