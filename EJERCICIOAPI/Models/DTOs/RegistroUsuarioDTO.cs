using System.ComponentModel.DataAnnotations;

namespace EJERCICIOAPI.Models.DTOs
{
    public class RegistroUsuarioDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
