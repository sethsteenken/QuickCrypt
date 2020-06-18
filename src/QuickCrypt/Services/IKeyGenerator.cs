namespace QuickCrypt
{
    /// <summary>
    /// Service to generate byte collection as a "key".
    /// </summary>
    public interface IKeyGenerator
    {
        /// <summary>
        /// Create unique key byte collection.
        /// </summary>
        /// <returns>Unique byte collection.</returns>
        byte[] Generate();
    }
}
