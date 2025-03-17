using System.Collections.Generic;
using JetBrains.Annotations;

namespace Modules.SaveLoad
{
    [UsedImplicitly]
    public sealed class GameSaveLoader
    {
        public class Params
        {
            public string SaveFileName;
            public string AutoSaveFileName;
        }
        
        private readonly IGameRepository _gameRepository;
        private readonly IEnumerable<IGameSerializer> _serializers;
        private readonly Params _params;

        public GameSaveLoader(
            IGameRepository gameRepository, 
            IEnumerable<IGameSerializer> serializers, Params parameters)
        {
            _gameRepository = gameRepository;
            _serializers = serializers;
            _params = parameters;
        }

        public void Save() => Save(_params.SaveFileName);

        public void AutoSave() => Save(_params.AutoSaveFileName);

        public void Load() => Load(_params.SaveFileName);

        public void LoadAutoSave() => Load(_params.AutoSaveFileName);

        private void Save(string fileName)
        {
            var gameState = new Dictionary<string, string>();
            foreach (IGameSerializer serializer in _serializers)
            {
                serializer.Serialize(gameState);
            }

            bool _ = _gameRepository.SetState(gameState, fileName);
        }

        private void Load(string fileName)
        {
            (Dictionary<string,string> gameState, bool ok) = _gameRepository.GetState(fileName);
            if (!ok) return;
            
            foreach (IGameSerializer serializer in _serializers)
            {
                serializer.Deserialize(gameState);
            }
        }
    }
}