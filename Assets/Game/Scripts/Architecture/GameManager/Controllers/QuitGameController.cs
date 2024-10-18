namespace Tavern.Architecture.GameManager.Controllers
{
    public sealed class QuitGameController  
    {
        private readonly GameManager _gameManager;

        public QuitGameController(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void OnQuit()
        {
            _gameManager.ExitGame();
        }
    }
}