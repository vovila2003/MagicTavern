using System.Collections.Generic;

namespace Modules.SaveLoad
{
    public interface IGameRepository
    {
        (Dictionary<string, string>, bool) GetState();
        bool SetState(Dictionary<string, string> gameState);
    }
}