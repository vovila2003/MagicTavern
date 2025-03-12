using System.Collections.Generic;
using System.IO;
using System.Text;
using JetBrains.Annotations;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Modules.SaveLoad
{
    [UsedImplicitly]
    public class GameRepository : IGameRepository
    {
        private readonly string _filePath;

        public GameRepository(string filePath)
        {
            _filePath = filePath;
        }

        public Dictionary<string, string> GetState()
        {
            if (!File.Exists(_filePath)) 
                return new Dictionary<string, string>();
            
            byte[] data = File.ReadAllBytes(_filePath);
            string json = Encoding.UTF8.GetString(data);
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            
            return result ?? new Dictionary<string, string>();
        }

        public void SetState(Dictionary<string, string> gameState)
        {
            string json = JsonConvert.SerializeObject(gameState);
            byte[] data = Encoding.UTF8.GetBytes(json);
            //TODO
            // add zip and crypt
            
            File.WriteAllBytes(_filePath, data);
        }
    }
}