using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Appointments.BL.Security
{
    public static class JWTAuthOptions
    {
        public const string ISSUER = "ForumApi";
        const string KEY = "authorizationKeyForumApi_366gh_123";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
