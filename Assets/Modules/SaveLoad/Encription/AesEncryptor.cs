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
        private Aes _aes;
        private readonly string _key;
        private readonly string _initVector;

        public AesEncryptor(AesData data)
        {
            _key = data.Key;
            _initVector = data.Iv;
        }

        public Stream Encrypt(Stream openStream)
        {
            _aes = SetupAes();
            _stream = new CryptoStream(openStream, 
                _aes.CreateEncryptor(_aes.Key, _aes.IV), CryptoStreamMode.Write);

            return _stream;
        }

        public Stream Decrypt(Stream closedStream)
        {
            _aes = SetupAes();
            _stream = new CryptoStream(closedStream, 
                _aes.CreateDecryptor(_aes.Key, _aes.IV), CryptoStreamMode.Read);
            
            return _stream;
        }

        public void Dispose()
        {
            _stream?.Dispose();
            _aes?.Dispose();
        }

        private Aes SetupAes()
        {
            var aes = Aes.Create();
            aes.Key = Encoding.ASCII.GetBytes(_key);
            aes.IV = Encoding.ASCII.GetBytes(_initVector);

            return aes;
        }
    }
}