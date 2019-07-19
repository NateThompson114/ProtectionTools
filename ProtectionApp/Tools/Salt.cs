using System;
using System.Security.Cryptography;

namespace ProtectionApp.Tools
{
    public class Salt
    {
        /// <summary>
        /// Creates salt for crypto items.
        /// </summary>
        /// <param name="maximumBits">Define the amount of bits, 8 16 24 32</param>
        /// <returns>Base 64 Salt String</returns>
        public static string Create(int maximumBits)
        {
            var randomBytes = new byte[maximumBits/8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return GetStringFromSalt(randomBytes);
            }
        }

        private static string GetStringFromSalt(byte[] saltBytes) => Convert.ToBase64String(saltBytes);
    }
}
