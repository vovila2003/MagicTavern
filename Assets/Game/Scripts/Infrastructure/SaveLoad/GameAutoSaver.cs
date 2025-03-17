using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Modules.SaveLoad;

namespace Tavern.Infrastructure
{
    [UsedImplicitly]
    public class GameAutoSaver : IDayBeginListener, IStartGameListener
    {
        private readonly GameSaveLoader _saveLoader;
        private readonly TimeGameCycle _gameCycle;

        public GameAutoSaver(GameSaveLoader saveLoader, TimeGameCycle gameCycle)
        {
            _saveLoader = saveLoader;
            _gameCycle = gameCycle;
        }

        void IDayBeginListener.OnDayBegin(int _)
        {
            if (_gameCycle.StartState) return;
            
            _saveLoader.AutoSave();
        }

        void IDayBeginListener.SetDay(int _) { }

        void IStartGameListener.OnStart()
        {
            _saveLoader.LoadAutoSave();
            _gameCycle.Timer.Start();
        }
    }
}