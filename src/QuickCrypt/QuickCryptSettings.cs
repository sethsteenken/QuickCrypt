using System.Security.Cryptography;

namespace QuickCrypt
{
    /// <summary>
    /// Settings used for cryptographic functions.
    /// Should generally be left to default values. Use <see cref="Default"/>.
    /// </summary>
    public sealed class QuickCryptSettings
    {
        public static readonly QuickCryptSettings Default = new QuickCryptSettings();

        public int BlockBitSize { get; set; } = 128;
        public int NonceBitSize { get; set; } = 128;
        public int MacBitSize { get; set; } = 128;
        public int KeyBitSize { get; set; } = 256;
        public int SaltBitSize { get; set; } = 64;
        public int Iterations { get; set; } = 10000;
        public int MinPasswordLength { get; set; } = 12;

        internal AesManaged ToAesManaged()
        {
            return new AesManaged()
            {
                KeySize = KeyBitSize,
                BlockSize = BlockBitSize,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };
        }
    }
}
