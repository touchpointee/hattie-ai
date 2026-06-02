using System;
using System.Security.Cryptography;
using System.Text;

namespace HattieAI.Infrastructure.Security
{
    public static class EncryptionHelper
    {
        private const int GcmIvLength = 12;
        private const int AuthTagLength = 16;

        public static string Encrypt(string plainText, string hexKey)
        {
            if (string.IsNullOrEmpty(plainText)) return string.Empty;
            if (string.IsNullOrEmpty(hexKey) || hexKey.Length != 64)
            {
                throw new ArgumentException("Encryption key must be a valid 64-character hex string (32 bytes).");
            }

            byte[] key = HexToBytes(hexKey);
            byte[] nonce = new byte[GcmIvLength];
            RandomNumberGenerator.Fill(nonce);

            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] cipherText = new byte[plainBytes.Length];
            byte[] tag = new byte[AuthTagLength];

            using (var aesGcm = new AesGcm(key, AuthTagLength))
            {
                aesGcm.Encrypt(nonce, plainBytes, cipherText, tag);
            }

            string ivHex = BytesToHex(nonce);
            string ctHex = BytesToHex(cipherText);
            string tagHex = BytesToHex(tag);

            return $"{ivHex}:{ctHex}:{tagHex}";
        }

        public static string Decrypt(string encryptedText, string hexKey)
        {
            if (string.IsNullOrEmpty(encryptedText)) return string.Empty;
            if (string.IsNullOrEmpty(hexKey) || hexKey.Length != 64)
            {
                throw new ArgumentException("Encryption key must be a valid 64-character hex string (32 bytes).");
            }

            var parts = encryptedText.Split(':');
            if (parts.Length != 3)
            {
                throw new ArgumentException("Encrypted text must be in the format 'iv-hex:ciphertext-hex:tag-hex'");
            }

            byte[] key = HexToBytes(hexKey);
            byte[] nonce = HexToBytes(parts[0]);
            byte[] cipherText = HexToBytes(parts[1]);
            byte[] tag = HexToBytes(parts[2]);

            if (nonce.Length != GcmIvLength)
            {
                throw new ArgumentException($"Invalid IV length: {nonce.Length}");
            }
            if (tag.Length != AuthTagLength)
            {
                throw new ArgumentException($"Invalid tag length: {tag.Length}");
            }

            byte[] plainBytes = new byte[cipherText.Length];

            using (var aesGcm = new AesGcm(key, AuthTagLength))
            {
                aesGcm.Decrypt(nonce, cipherText, tag, plainBytes);
            }

            return Encoding.UTF8.GetString(plainBytes);
        }

        private static byte[] HexToBytes(string hex)
        {
            int numberChars = hex.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        private static string BytesToHex(byte[] bytes)
        {
            var sb = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
