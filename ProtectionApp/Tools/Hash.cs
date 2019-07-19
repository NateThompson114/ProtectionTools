using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ProtectionApp.Tools
{
    internal static class Hash
    {
        /// <summary>
        /// Create a Hash of a value
        /// </summary>
        /// <param name="value">Value to encrypt</param>
        /// <param name="salt">Salt</param>
        /// <param name="hashBitLength">Bit length 64,128,384,512</param>
        /// <returns>Base 64 String</returns>
        public static string Create(string value, string salt, int hashBitLength) => Convert.ToBase64String(HashBytes(value, salt, hashBitLength));

        /// <summary>
        /// Create a Hash of a value
        /// </summary>
        /// <param name="value">Value to encrypt</param>
        /// <param name="salt">Salt</param>
        /// <param name="hashBitLength">Bit length 64,128,384,512</param>
        /// <returns>Hexadecimal String</returns>
        public static string CreateWeb(string value, string salt, int hashBitLength) => string.Join("", HashBytes(value, salt, hashBitLength).Select(c => ((int)c).ToString("X2")));

        public static string HashThis(this string value, string salt, int hashByteLength) => Create(value, salt, hashByteLength);

        public static string HashThisWeb(this string value, string salt, int hashByteLength) => CreateWeb(value, salt, hashByteLength);

        public static bool Validate(string value, string salt, string hash, int hashByteLength) => Create(value, salt, hashByteLength) == hash;

        public static bool ValidateThisWeb(string value, string salt, string hash, int hashByteLength) => CreateWeb(value, salt, hashByteLength) == hash;

        private static byte[] HashBytes(string value, string salt, int hashBitLength) => 
            KeyDerivation.Pbkdf2(value, Encoding.UTF8.GetBytes(salt), KeyDerivationPrf.HMACSHA512, 10000, hashBitLength / 8);
    }
}
