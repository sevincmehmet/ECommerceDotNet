using Entities.Concrete.Users;
using Infrastructure.Security.Abstract;
using Infrastructure.Security.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Security.Concrete
{
    public class JwtHelper : ITokenHelper
    {
        private readonly TokenOptions _tokenOptions;

        // IOptions yerine direkt nesneyi enjekte ediyoruz
        public JwtHelper(TokenOptions tokenOptions)
        {
            _tokenOptions = tokenOptions;
        }

        public AccessToken CreateToken(User user, List<string> roles, List<string> permissions)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, user.Email)
            };

            // 1. Rolleri standart Role claim tipiyle ekliyoruz
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // 2. Yetkileri (Permissions) özel bir claim tipiyle ekliyoruz
            // Bu sayede Middleware "permission" etiketli tüm claimleri okuyabilecek
            claims.AddRange(permissions.Select(p => new Claim("permission", p)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration),
                SigningCredentials = signingCredentials,
                Issuer = _tokenOptions.Issuer,
                Audience = _tokenOptions.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AccessToken(
                token: tokenHandler.WriteToken(token),
                expiration: tokenDescriptor.Expires.Value
            );
        }
    }
}







