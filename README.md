# QuickCrypt
> .NET library providing quick and simple symmetric cryptographic functions using Simple Encryption (AES) and Authentication (HMAC). 

QuickCrypt is a simple .NET class library targeting the .NET Standard 2.0. Provides interfaces and implementation for encrypting and decrypting bytes and strings using symmetric cryptography. Also provides a random key generator to use during the cryptographic process.

## Installing and Setup

QuickCrypt will soon be available as a [Nuget Package].  Add to any .NET project (supporting .NET Standard 2.0) via Nuget Package Manager in Visual Studio or via Package Manager Console.

### Service Collection Configuration

QuickCrypt includes extensions to add its interfaces and default implementation to `IServiceCollection`:

```csharp
services.AddQuickCrypt();
```

Default implementation will set the credentials/key for cryptography functions from a RandomNumberGenerator value per scope of the service provider.

Optionally provide configuration values that include custom credentials/key and settings (bit sizes, iterations, etc.) used for all QuickCrypt cryptography functions in the application:

```csharp
services.AddQuickCrypt(new AesHmacCredentials(key: "MY_KEY", authKey: "MY_AUTH_KEY"), new QuickCryptSettings() { ... });
```


## Use
Inject QuickCrypt interface `IEncryptor` to encrypt a payload of string or bytes.  Inject QuickCrypt interface `IDecryptor` to decrypt a payload of string or bytes.

```csharp
public class SecureService
{
    private readonly IEncryptor _encryptor;
    private readonly IDecryptor _decryptor;
  
    public SecureService(IEncryptor encryptor, IDecryptor decryptor)
    {
        _encryptor = encryptor;
        _decryptor = decryptor;
    }
    
    public void DoWork()
    {
        // examples using byte array:
        
        byte[] bytesToEncrypt = GetSomeBytesToEncrypt();
        
        byte[] encryptedBytes = _encryptor.Encrypt(bytesToEncrypt);
        
        byte[] decryptedBytes = _decryptor.Decrypt(encryptedBytes);
        
        // examples using string:
        
        string stringToEncrypt = GetSomeStringToEncrypt();
        
        string encryptedString = _encrypt.EncryptToString(stringToEncrypt);
        
        string decryptedString = _decrypt.DecryptToString(encryptedString);
        
        // NOTE: byte array functions AND string functions can also convert from one or the other
    }
    
    ...
}
```

## Contributing

If you'd like to contribute, please fork the repository and use a feature
branch. Pull requests are more than welcome!


## Licensing

The code in this project is licensed under MIT license. [View the license here](LICENSE.md)
