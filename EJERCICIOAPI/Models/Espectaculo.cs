using System.Security.Cryptography;
using System.Text;

namespace EJERCICIOAPI.Models
{
    public class Espectaculo
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime Fechayhora { get; set; }
        public Artista? Artista { get; set; } 

        public int ArtistaId { get; set; }

    }
}
