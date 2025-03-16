using System.Collections.Generic;
using JetBrains.Annotations;

namespace Modules.SaveLoad
{
    [UsedImplicitly]
    public sealed class GameSaveLoader
    {
        private readonly IGameRepository _gameRepository;
        private readonly IEnumerable<IGameSerializer> _serializers;

        public GameSaveLoader(
            IGameRepository gameRepository, 
            IEnumerable<IGameSerializer> serializers)
        {
            _gameRepository = gameRepository;
            _serializers = serializers;
        }

        public void Save()
        {
            var gameState = new Dictionary<string, string>();
            foreach (IGameSerializer serializer in _serializers)
            {
                serializer.Serialize(gameState);
            }

            bool _ = _gameRepository.SetState(gameState);
        }

        public void Load()
        {
            (Dictionary<string,string> gameState, bool ok) = _gameRepository.GetState();
            if (!ok) return;
            
            foreach (IGameSerializer serializer in _serializers)
            {
                serializer.Deserialize(gameState);
            }
        }
    }
}