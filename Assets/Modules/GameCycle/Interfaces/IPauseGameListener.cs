//using Tavern.Architecture.GameManager.Interfaces;

namespace Modules.GameCycle.Interfaces
{
    public interface IPauseGameListener : IGameListener
    {
        void OnPause();
    }
}