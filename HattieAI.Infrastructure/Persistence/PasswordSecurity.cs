using System;
using System.Security.Cryptography;

namespace HattieAI.Infrastructure.Persistence
{
    public static class PasswordSecurity
    {
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

            // Generate a 128-bit salt
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Derive a 160-bit subkey (hash) using PBKDF2 with 10,000 iterations and SHA256
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(20);
                
                // Combine salt and hash into a 36-byte array
                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password)) return false;
            if (string.IsNullOrEmpty(hashedPassword)) return false;

            try
            {
                byte[] hashBytes = Convert.FromBase64String(hashedPassword);
                if (hashBytes.Length != 36) return false;

                // Extract salt
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);

                // Compute hash of the input password using the extracted salt
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
                {
                    byte[] hash = pbkdf2.GetBytes(20);
                    
                    // Constant-time comparison to prevent timing attacks
                    int diff = 0;
                    for (int i = 0; i < 20; i++)
                    {
                        diff |= hashBytes[i + 16] ^ hash[i];
                    }
                    return diff == 0;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
