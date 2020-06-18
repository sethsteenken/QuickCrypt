using System;

namespace QuickCrypt
{
    public static class KeyGeneratorExtensions
    {
        /// <summary>
        /// Create new instance of <see cref="Credentials"/> by using key generator to create new keys.
        /// </summary>
        /// <param name="keyGenerator"></param>
        /// <param name="includeNonSecretPayload">Optional flag to include generating a key for the optional non-secret payload.</param>
        /// <returns></returns>
        public static Credentials CreateCredentials(this IKeyGenerator keyGenerator, bool includeNonSecretPayload = false)
        {
            if (keyGenerator == null)
                throw new ArgumentNullException(nameof(keyGenerator));

            return new Credentials(key: keyGenerator.Generate(),
                                   nonSecretPayload: includeNonSecretPayload ? keyGenerator.Generate() : null);
        }

        /// <summary>
        /// Create new instance of <see cref="AesHmacCredentials"/> by using key generator to create new keys.
        /// </summary>
        /// <param name="keyGenerator"></param>
        /// <param name="includeNonSecretPayload">Optional flag to include generating a key for the optional non-secret payload.</param>
        /// <returns></returns>
        public static AesHmacCredentials CreateAesHmacCredentials(this IKeyGenerator keyGenerator, bool includeNonSecretPayload = false)
        {
            if (keyGenerator == null)
                throw new ArgumentNullException(nameof(keyGenerator));

            return new AesHmacCredentials(key: keyGenerator.Generate(),
                                          authKey: keyGenerator.Generate(),
                                          nonSecretPayload: includeNonSecretPayload ? keyGenerator.Generate() : null);
        }
    }
}
