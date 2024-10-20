namespace Tavern.Architecture
{
    public sealed class QuitGameController  
    {
        private readonly GameCycleController _gameCycle;

        public QuitGameController(GameCycleController gameCycle)
        {
            _gameCycle = gameCycle;
        }

        public void OnQuit()
        {
            _gameCycle.ExitGame();
        }
    }
}