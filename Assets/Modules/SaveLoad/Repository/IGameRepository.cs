using System.Collections.Generic;

namespace Modules.SaveLoad
{
    public interface IGameRepository
    {
        (Dictionary<string, string>, bool) GetState(string fileName);
        bool SetState(Dictionary<string, string> gameState, string fileName);
    }
}