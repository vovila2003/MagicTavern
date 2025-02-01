namespace Tavern.Infrastructure
{
    public sealed class StartGameController
    {
        private readonly GameCycleController _gameCycle;

        public StartGameController(GameCycleController gameCycle)
        {
            _gameCycle = gameCycle;
        }

        public void OnStart()
        {
            _gameCycle.PrepareGame();
            _gameCycle.StartGame();
        }
    }
}