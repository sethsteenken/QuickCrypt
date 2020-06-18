using Microsoft.Extensions.DependencyInjection;
using System;

namespace QuickCrypt
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add cryptography services for encrypting and decrypting string and byte collections.
        /// Uses symmetrical <see cref="AesHmacCryptographer"/> for Simple Encryption (AES) then Authentication (HMAC).
        /// Reference - https://stackoverflow.com/a/10366194/9882811
        /// </summary>
        /// <param name="services">Existing service collection.</param>
        /// <param name="credentials">Keys and optional payload additions for encrypt and decrypt actions. By default, will generate new keys using <see cref="IKeyGenerator"/> per scope.</param>
        /// <param name="settings">Optional custom values used for cryptography actions. Default values will be applied via <see cref="QuickCryptSettings.Default"/> and should typically be left to the defaults.</param>
        /// <returns></returns>
        public static IServiceCollection AddQuickCrypt(
            this IServiceCollection services, 
            AesHmacCredentials credentials = null, 
            QuickCryptSettings settings = null)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (settings == null)
                settings = QuickCryptSettings.Default;

            services.AddSingleton<QuickCryptSettings>(settings);
            services.AddSingleton<IKeyGenerator, RandomNumberKeyGenerator>();

            if (credentials != null)
            {
                services.AddSingleton<AesHmacCredentials>(credentials);
            }
            else
            {
                services.AddScoped<AesHmacCredentials>(serviceProvider =>
                {
                    return serviceProvider.GetRequiredService<IKeyGenerator>()
                                          .CreateAesHmacCredentials();
                });
            }

            services.AddScoped<AesHmacCryptographer>();
            services.AddScoped<IDecryptor>(serviceProvider => serviceProvider.GetRequiredService<AesHmacCryptographer>());
            services.AddScoped<IEncryptor>(serviceProvider => serviceProvider.GetRequiredService<AesHmacCryptographer>());

            return services;
        }
    }
}
