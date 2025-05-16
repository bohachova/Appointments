

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Appointments.BL.Security
{
    public class JWTTokenGenerator
    {
        public static string GetToken(int id)
        {
            var claims = new List<Claim> {
                new Claim("UserId",id.ToString())
            };
            var jwt = new JwtSecurityToken(
            issuer: JWTAuthOptions.ISSUER,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(60)),
                    signingCredentials: new SigningCredentials(JWTAuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
