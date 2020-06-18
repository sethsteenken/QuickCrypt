using System;
using System.Text;

namespace QuickCrypt
{
    public static class EncryptorExtensions
    {
        /// <summary>
        /// Encrypt string message and output in byte[] format.
        /// String converted to bytes via <see cref="Encoding.UTF8"/>.
        /// </summary>
        /// <param name="encrytor"></param>
        /// <param name="message">Message to encrypt.</param>
        /// <returns></returns>
        public static byte[] Encrypt(this IEncryptor encrytor, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException(nameof(message));

            return encrytor?.Encrypt(Encoding.UTF8.GetBytes(message));
        }

        /// <summary>
        /// Encrypt byte[] message and output in Base64 string format.
        /// </summary>
        /// <param name="encrytor"></param>
        /// <param name="message">Message to encrypt.</param>
        /// <returns></returns>
        public static string EncryptToString(this IEncryptor encrytor, byte[] message)
        {
            var result = encrytor?.Encrypt(message);
            return result == null ? null : Convert.ToBase64String(result);
        }

        /// <summary>
        /// Encrypt string message and output in Base64 string format.
        /// </summary>
        /// <param name="encrytor"></param>
        /// <param name="message">Message to encrypt.</param>
        /// <returns></returns>
        public static string EncryptToString(this IEncryptor encrytor, string message)
        {
            var result = encrytor?.Encrypt(message);
            return result == null ? null : Convert.ToBase64String(result);
        }
    }
}
