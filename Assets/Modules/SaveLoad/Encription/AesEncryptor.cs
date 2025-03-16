using System.IO;
using System.Security.Cryptography;
using System.Text;
using JetBrains.Annotations;

namespace Modules.SaveLoad
{
    [UsedImplicitly]
    public class AesEncryptor : IEncryptor
    {
        public struct AesData
        {
            public string Key;
            public string Iv;
        }
        
        private Stream _stream;
        private AesCryptoServiceProvider _cryptoServiceProvider;
        private readonly string _key;
        private readonly string _initVector;

        public AesEncryptor(AesData data)
        {
            _key = data.Key;
            _initVector = data.Iv;
        }

        public Stream Encrypt(Stream openStream)
        {
            _cryptoServiceProvider = SetupCryptoServiceProvider();
            
            _stream = new CryptoStream(openStream, 
                _cryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
            
            return _stream;
        }

        public Stream Decrypt(Stream closedStream)
        {
            _cryptoServiceProvider = SetupCryptoServiceProvider();

            _stream = new CryptoStream(closedStream, 
                _cryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Read);
            
            return _stream;
        }

        public void Dispose()
        {
            _stream?.Dispose();
            _cryptoServiceProvider?.Dispose();
        }

        private AesCryptoServiceProvider SetupCryptoServiceProvider()
        {
            var cryptoServiceProvider = new AesCryptoServiceProvider();
            cryptoServiceProvider.Key = Encoding.ASCII.GetBytes(_key);
            cryptoServiceProvider.IV = Encoding.ASCII.GetBytes(_initVector);

            return cryptoServiceProvider;
        }
    }
}