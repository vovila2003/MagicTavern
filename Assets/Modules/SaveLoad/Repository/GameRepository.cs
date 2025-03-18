using System;
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
            public bool UseCompression;
            public bool UseEncryption;
        }
        
        private readonly IEncryptor _encryptor;
        private readonly bool _useCompression;
        private readonly bool _useEncryption;

        public GameRepository(IEncryptor encryptor, Params parameters)
        {
            _encryptor = encryptor;
            _useCompression = parameters.UseCompression;
            _useEncryption = parameters.UseEncryption;
        }

        public bool SetState(Dictionary<string, string> gameState, string fileName)
        {
            try
            {
                using FileStream fileStream = File.Create(fileName);
                Stream innerStream = fileStream;
                if (_useEncryption) 
                    innerStream = _encryptor.Encrypt(innerStream);

                if (_useCompression) 
                    innerStream = new GZipStream(innerStream, CompressionMode.Compress);

                using var binaryWriter = new BinaryWriter(innerStream);
                string json = JsonConvert.SerializeObject(gameState);
                binaryWriter.Write(json);

            }
            catch (Exception e)
            {
                Debug.Log($"Write file ERROR: {e}");
                return false;
            }
            
            Debug.Log($"Write file OK: {fileName}");
            
            return true;
        }

        public (Dictionary<string, string>, bool) GetState(string fileName)
        {
            if (!File.Exists(fileName)) 
                return (new Dictionary<string, string>(), false);
            
            try
            {
                using FileStream fileStream = File.Open(fileName, FileMode.Open);
                Stream innerStream = fileStream;
                if (_useEncryption) 
                    innerStream = _encryptor.Decrypt(innerStream);

                if (_useCompression) 
                    innerStream = new GZipStream(innerStream, CompressionMode.Decompress);
                
                using var binaryReader = new BinaryReader(innerStream);
                string data = binaryReader.ReadString();
                var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
                Debug.Log($"Read file OK: {fileName}");

                return result is not null ? (result, true) : (new Dictionary<string, string>(), false);
            }
            catch (Exception e)
            {
                Debug.Log($"Read file ERROR: {e}");
                return (new Dictionary<string, string>(), false);
            }
        }
    }
}