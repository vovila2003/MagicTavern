namespace Tavern.Architecture.GameManager.Controllers
{
    public sealed class StartGameController
    {
        private readonly GameManager _gameManager;

        public StartGameController(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void OnStart()
        {
            _gameManager.PrepareGame();
            _gameManager.StartGame();
        }
    }
}