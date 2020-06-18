namespace QuickCrypt
{
    /// <summary>
    /// Service for decrypting encrypted messages.
    /// </summary>
    public interface IDecryptor
    {
        /// <summary>
        /// Decrypt encrypted message <paramref name="message"/>.
        /// </summary>
        /// <param name="message">Encrypted message to decrypt.</param>
        /// <returns></returns>
        byte[] Decrypt(byte[] message);
    }
}
