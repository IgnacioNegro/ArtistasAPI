using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using JwtClaim = System.Security.Claims.Claim;
using EJERCICIOAPI.Models;  


    namespace EJERCICIOAPI.Helpers
    {
        public class Autentica
        {
            private readonly IConfiguration _config;

            public Autentica(IConfiguration config)
            {
                _config = config;
            }

            public string CrearToken(Usuario usuario)
            {
                // 1) Leer configuración de JWT
                var key = _config["Jwt:Key"];
                var issuer = _config["Jwt:Issuer"];
                var audience = _config["Jwt:Audience"];
                var hours = int.Parse(_config["Jwt:DurationHours"] ?? "2");

                // 2) Crear claims
                var claims = new List<JwtClaim>
            {
                new JwtClaim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                new JwtClaim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new JwtClaim(JwtRegisteredClaimNames.UniqueName, usuario.Email),
                new JwtClaim(
                    JwtRegisteredClaimNames.Iat,
                    DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64
                )
            };

                // 3) Generar clave y credenciales
                var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var creds = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

                // 4) Fecha de expiración
                var expires = DateTime.UtcNow.AddHours(hours);

                // 5) Construir el token
                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: expires,
                    signingCredentials: creds
                );

                // 6) Serializar y devolver
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }
