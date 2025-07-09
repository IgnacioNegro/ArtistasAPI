using System.ComponentModel.DataAnnotations;

namespace EJERCICIOAPI.Models.DTOs
{
    public class RespuestaLoginTokenDTO
    {
        [Required]
        public string Token { get; set; } = string.Empty;
    }
}
