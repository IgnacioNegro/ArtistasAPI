using System.ComponentModel.DataAnnotations;
namespace EJERCICIOAPI.Models.DTOs
{
    public class CategoriaDTO
    {

        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

    

       
    }
}
