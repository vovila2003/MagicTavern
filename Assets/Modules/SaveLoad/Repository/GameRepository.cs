using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using JetBrains.Annotations;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Modules.SaveLoad
{
    [UsedImplicitly]
    public class GameRepository : IGameRepository
    {
        public struct Params
        {
            public string FileName;
            public bool UseCompression;
            public bool UseEncryption;
        }
        
        private readonly IEncryptor _encryptor;
        private readonly string _filePath;
        private readonly bool _useCompression;
        private readonly bool _useEncryption;

        public GameRepository(IEncryptor encryptor, Params parameters)
        {
            _encryptor = encryptor;
            _filePath = parameters.FileName;
            _useCompression = parameters.UseCompression;
            _useEncryption = parameters.UseEncryption;
        }

        public Dictionary<string, string> GetState()
        {
            if (!File.Exists(_filePath)) 
                return new Dictionary<string, string>();
            
            using FileStream fileStream = File.Open(_filePath, FileMode.Open);
            using Stream cryptoStream = _encryptor.Decrypt(fileStream);
            using Stream zipStream = new GZipStream(cryptoStream, CompressionMode.Decompress);
            using var binaryReader = new BinaryReader(zipStream);
            string data = binaryReader.ReadString();
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            Debug.Log($"Read file {_filePath}");
            
            return result ?? new Dictionary<string, string>();;
        }

        public void SetState(Dictionary<string, string> gameState)
        {
            using FileStream fileStream = File.Create(_filePath);
            using Stream cryptoStream = _encryptor.Encrypt(fileStream);
            using Stream zipStream = new GZipStream(cryptoStream, CompressionMode.Compress);
            using var binaryWriter = new BinaryWriter(zipStream);
            string json = JsonConvert.SerializeObject(gameState);
            binaryWriter.Write(json);

            Debug.Log($"Write file: {_filePath}");
        }
    }
}