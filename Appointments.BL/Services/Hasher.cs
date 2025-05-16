using Appointments.BL.Abstracts;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Appointments.BL.Services
{
    public class Hasher : IHasher
    {
        public string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            byte[] hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100_000,
                numBytesRequested: 32 
            );

            byte[] hashBytes = new byte[salt.Length + hash.Length];
            Array.Copy(salt, 0, hashBytes, 0, salt.Length);
            Array.Copy(hash, 0, hashBytes, salt.Length, hash.Length);

            return Convert.ToBase64String(hashBytes);
        }


        public bool Verify(string password, string savedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(savedHash);

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, salt.Length);

            byte[] hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100_000,
                numBytesRequested: 32
            );

            byte[] originalHash = new byte[32];
            Array.Copy(hashBytes, salt.Length, originalHash, 0, originalHash.Length);

            return CryptographicOperations.FixedTimeEquals(originalHash, hash);
        }
    }
}
