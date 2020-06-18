using System;
using System.IO;
using System.Security.Cryptography;

namespace QuickCrypt
{
    /// <summary>
    /// Default encryption and decryption provider. 
    /// Executes symmetrical cryptographic processes using Simple Encryption (AES) then Authentication (HMAC).
    /// </summary>
    public class AesHmacCryptographer : IEncryptor, IDecryptor
    {
        // origin reference - https://stackoverflow.com/a/10366194/9882811

        private readonly QuickCryptSettings _settings;
        private readonly AesHmacCredentials _credentials;

        public AesHmacCryptographer(
            QuickCryptSettings settings, 
            AesHmacCredentials credentials)
        {
            _settings = settings;
            _credentials = credentials;

            _credentials.Validate(_settings);
        }

        public virtual byte[] Encrypt(byte[] message)
        {
            if (message == null || message.Length < 1)
                throw new ArgumentNullException(nameof(message));

            byte[] cipherText;
            byte[] iv;

            using (AesManaged aes = _settings.ToAesManaged())
            {
                // use random IV
                aes.GenerateIV();
                iv = aes.IV;

                using (var encrypter = aes.CreateEncryptor(_credentials.Key, iv))
                {
                    using (var cipherStream = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(cipherStream, encrypter, CryptoStreamMode.Write))
                        {
                            using (var binaryWriter = new BinaryWriter(cryptoStream))
                            {
                                // encrypt Data
                                binaryWriter.Write(message);
                            }
                        }
                        
                        cipherText = cipherStream.ToArray();
                    }
                }
            }

            // assemble encrypted message and add authentication
            using (var hmac = new HMACSHA256(_credentials.AuthKey))
            {
                using (var encryptedStream = new MemoryStream())
                {
                    using (var binaryWriter = new BinaryWriter(encryptedStream))
                    {
                        // prepend non-secret payload if any
                        binaryWriter.Write(_credentials.NonSecretPayload);
                        
                        // prepend IV
                        binaryWriter.Write(iv);
                        
                        // write Ciphertext
                        binaryWriter.Write(cipherText);
                        binaryWriter.Flush();

                        // authenticate all data
                        var tag = hmac.ComputeHash(encryptedStream.ToArray());

                        // postpend tag
                        binaryWriter.Write(tag);
                    }

                    return encryptedStream.ToArray();
                }
            }
        }

        public virtual byte[] Decrypt(byte[] message)
        {
            if (message == null || message.Length < 1)
                throw new ArgumentNullException(nameof(message));

            int nonSecretPayloadLength = _credentials.NonSecretPayload.Length;

            using (var hmac = new HMACSHA256(_credentials.AuthKey))
            {
                var sentTag = new byte[hmac.HashSize / 8];

                // calculate Tag
                var calcTag = hmac.ComputeHash(message, 0, message.Length - sentTag.Length);
                var ivLength = (_settings.BlockBitSize / 8);

                if (message.Length < sentTag.Length + nonSecretPayloadLength + ivLength)
                    return null;

                // grab Sent Tag
                Array.Copy(message, message.Length - sentTag.Length, sentTag, 0, sentTag.Length);

                // compare Tag with constant time comparison
                var compare = 0;
                for (var i = 0; i < sentTag.Length; i++)
                    compare |= sentTag[i] ^ calcTag[i];

                // message does not authenticate
                if (compare != 0)
                    return null;

                using (AesManaged aes = _settings.ToAesManaged())
                {
                    // grab IV from message
                    var iv = new byte[ivLength];
                    Array.Copy(message, nonSecretPayloadLength, iv, 0, iv.Length);

                    using (var decrypter = aes.CreateDecryptor(_credentials.Key, iv))
                    {
                        using (var plainTextStream = new MemoryStream())
                        {
                            using (var decrypterStream = new CryptoStream(plainTextStream, decrypter, CryptoStreamMode.Write))
                            {
                                using (var binaryWriter = new BinaryWriter(decrypterStream))
                                {
                                    // decrypt cipher text from message
                                    binaryWriter.Write(
                                        message,
                                        nonSecretPayloadLength + iv.Length,
                                        message.Length - nonSecretPayloadLength - iv.Length - sentTag.Length
                                    );
                                }

                                // return decrypted plain text
                                return plainTextStream.ToArray();
                            }
                        }
                    }
                }
            }
        }
    }
}
