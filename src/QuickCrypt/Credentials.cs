using System;

namespace QuickCrypt
{
    /// <summary>
    /// Generic cryptography credentials housing a key to use for encryption/decryption.
    /// </summary>
    public class Credentials
    {
        public Credentials(byte[] key, byte[] nonSecretPayload = null)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            NonSecretPayload = nonSecretPayload ?? new byte[] { };
        }

        public Credentials (string key, string nonSecretPayload = null)
            : this (string.IsNullOrWhiteSpace(key) ? null : Convert.FromBase64String(key),
                    string.IsNullOrWhiteSpace(nonSecretPayload) ? null : Convert.FromBase64String(nonSecretPayload))
        {

        }

        /// <summary>
        /// Required key used for cryptographic functions.
        /// </summary>
        public byte[] Key { get; }

        /// <summary>
        /// Optional non-secret payload used duing cryptographic functions.
        /// </summary>
        public byte[] NonSecretPayload { get; }

        /// <summary>
        /// Validate credentials based on current settings <paramref name="settings"/>.
        /// </summary>
        /// <param name="settings"></param>
        /// <exception cref="ArgumentException"></exception>
        public virtual void Validate(QuickCryptSettings settings)
        {
            if (Key == null || Key.Length != settings.KeyBitSize / 8)
                throw new ArgumentException($"Key invalid. Key needs to be {settings.KeyBitSize} bit.", "Key");
        }
    }
}
