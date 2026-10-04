using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace AccessPath.Data.Security
{
    public class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 600000;

        public static string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSize);

            string saltBase64 = Convert.ToBase64String(salt);

            string hashBase64 = Convert.ToBase64String(hash);

            return $"PBKDF2${Iterations}${saltBase64}${hashBase64}";
        }

        public static bool VerifyPassword(string password, string storedPasswordHash)
        {
            string[] parts = storedPasswordHash.Split('$');

            if (parts.Length != 4)
            {
                return false;
            }

            if (parts[0] != "PBKDF2")
            {
                return false;
            }

            if (!int.TryParse(parts[1], out int iterations))
            {
                return false;
            }

            try
            {
                byte[] salt = Convert.FromBase64String(parts[2]);

                byte[] storedHash = Convert.FromBase64String(parts[3]);

                byte[] enteredPasswordHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, storedHash.Length);

                return CryptographicOperations.FixedTimeEquals(enteredPasswordHash, storedHash);
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
