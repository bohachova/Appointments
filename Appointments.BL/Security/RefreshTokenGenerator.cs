
using System.Security.Cryptography;

namespace Appointments.BL.Security
{
    public class RefreshTokenGenerator
    {
        public static string GetRefreshToken()
        {
            byte[] randomBytes = new byte[64];
            RandomNumberGenerator.Fill(randomBytes); 
            return Convert.ToBase64String(randomBytes);
        }
    }
}
