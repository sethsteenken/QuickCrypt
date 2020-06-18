using System;

namespace QuickCrypt
{
    /// <summary>
    /// Credentials holding cryptography key (for AES) and authentiction key (for HMAC).
    /// </summary>
    public sealed class AesHmacCredentials : Credentials
    {
        public AesHmacCredentials(byte[] key, byte[] authKey, byte[] nonSecretPayload = null) 
            : base(key, nonSecretPayload)
        {
            AuthKey = authKey ?? throw new ArgumentNullException(nameof(authKey));
        }

        public AesHmacCredentials(string key, string authKey, string nonSecretPayload = null)
            : base(key, nonSecretPayload)
        {
            AuthKey = string.IsNullOrWhiteSpace(authKey) ? throw new ArgumentNullException(nameof(authKey)) 
                : Convert.FromBase64String(authKey);
        }

        /// <summary>
        /// Authentication key used to verify integrity of source.
        /// </summary>
        public byte[] AuthKey { get; }

        /// <summary>
        /// Validate credentials based on current settings <paramref name="settings"/>.
        /// </summary>
        /// <param name="settings"></param>
        /// <exception cref="ArgumentException"></exception>
        public override void Validate(QuickCryptSettings settings)
        {
            base.Validate(settings);

            if (AuthKey == null || AuthKey.Length != settings.KeyBitSize / 8)
                throw new ArgumentException($"AuthKey invalid. AuthKey needs to be {settings.KeyBitSize} bit.", "AuthKey");
        }
    }
}
