using Microsoft.IdentityModel.Tokens;
using SGEServer.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Logging;

namespace SGEServer.Auth
{
    public class TokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(LoginModel Acessos)
        {
            List<Claim> claims = new()
            {
                new Claim("UserId", Acessos.CodUsuario.ToString()),
                new Claim("UserLogin", Acessos.Usuario!.ToString()),
                new Claim("UserNome", Acessos.Nome!.ToString()),
                new Claim("UserIdEmpresa", Acessos.CodEmpresa.ToString()),
                new Claim("UserEmpresa", Acessos.Empresa!.ToString())
            };

            //// LISTA DE PÁGINAS QUE O USER TEM ACESSO
            //List<int> Paginas = new List<int>();

            //foreach (var item in Acessos.AcessosLista!)
            //{
            //    if (!Paginas.Contains(int.Parse(item.Id.ToString())))
            //    {
            //        Paginas.Add(int.Parse(item.Id.ToString()));
            //        claims.Add(new Claim(ClaimTypes.Role, item.Id.ToString()));
            //    }
            //}

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(null,
                null,
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public IEnumerable<Claim> GetClaims(string token)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))
            };

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out validatedToken);

            if (validatedToken is JwtSecurityToken jwtToken)
            {
                if (jwtToken.ValidTo < DateTime.UtcNow)
                {
                    throw new SecurityTokenExpiredException("Token expirado");
                }
            }

            return principal.Claims;
        }
    }
}
