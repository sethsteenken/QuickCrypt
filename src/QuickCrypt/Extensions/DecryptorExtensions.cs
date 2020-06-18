using System;
using System.Text;

namespace QuickCrypt
{
    public static class DecryptorExtensions
    {
        /// <summary>
        /// Decrypt encrypted string message to the original message in byte[] format.
        /// </summary>
        /// <param name="decryptor"></param>
        /// <param name="message">Encrypted message to decrypt. Must be a valid Base64 string.</param>
        /// <returns></returns>
        public static byte[] Decrypt(this IDecryptor decryptor, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            return decryptor?.Decrypt(Convert.FromBase64String(message));
        }

        /// <summary>
        /// Decrypt encrypted byte[] message to the original message as string. 
        /// Converted to string using <see cref="Encoding.UTF8"/>.
        /// </summary>
        /// <param name="decryptor"></param>
        /// <param name="message">Encrypted message to decrypt.</param>
        /// <returns></returns>
        public static string DecryptToString(this IDecryptor decryptor, byte[] message)
        {
            var result = decryptor?.Decrypt(message);
            return result == null ? null : Encoding.UTF8.GetString(result);
        }

        /// <summary>
        /// Decrypt encrypted string message to the original message as string. 
        /// Converted to string using <see cref="Encoding.UTF8"/>. 
        /// </summary>
        /// <param name="decryptor"></param>
        /// <param name="message">Encrypted message to decrypt.</param>
        /// <returns></returns>
        public static string DecryptToString(this IDecryptor decryptor, string message)
        {
            var result = decryptor?.Decrypt(message);
            return result == null ? null : Encoding.UTF8.GetString(result);
        }
    }
}
