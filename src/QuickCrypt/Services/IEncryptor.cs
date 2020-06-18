namespace QuickCrypt
{
    /// <summary>
    /// Service for encrypting messages.
    /// </summary>
    public interface IEncryptor
    {
        /// <summary>
        /// Encrypt message <paramref name="message"/>.
        /// </summary>
        /// <param name="message">Message to encrypt.</param>
        /// <returns></returns>
        byte[] Encrypt(byte[] message);
    }
}
