using System.Collections.Generic;

namespace Modules.SaveLoad
{
    public interface IGameRepository
    {
        Dictionary<string, string> GetState();
        void SetState(Dictionary<string, string> gameState);
    }
}