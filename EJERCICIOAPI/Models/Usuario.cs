using System.Security.Cryptography;
using System.Text;

namespace EJERCICIOAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordEncriptado { get; set; }
        public List<Artista> Artistas { get; set; } = new List<Artista>();

        public Usuario() { }
        public Usuario(string email, string passwordSinEncriptar)
        {
            Email = email;
            SetearPasswordEncriptado(passwordSinEncriptar);
        }

        private void SetearPasswordEncriptado(string password)
        {
            PasswordEncriptado = EncriptarPassword(password);
        }
        public static string EncriptarPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                // Convertir a cadena hexadecimal
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2")); // X2 para hex en mayúsculas
                }
                return sb.ToString();
            }
        }

    }
}
