using System.Security.Cryptography;

namespace QuickCrypt
{
    /// <summary>
    /// Service to generate unique key using <see cref="RandomNumberGenerator"/>.
    /// Byte array size determined by <see cref="QuickCryptSettings.KeyBitSize"/>.
    /// </summary>
    public class RandomNumberKeyGenerator : IKeyGenerator
    {
        private static readonly RandomNumberGenerator _random = RandomNumberGenerator.Create();
        private readonly QuickCryptSettings _settings;

        public RandomNumberKeyGenerator(QuickCryptSettings settings)
        {
            _settings = settings;
        }

        public byte[] Generate()
        {
            var key = new byte[_settings.KeyBitSize / 8];
            _random.GetBytes(key);
            return key;
        }
    }
}
