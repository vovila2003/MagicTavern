namespace Architecture.Controllers
{
    public sealed class PauseGameController 
    {
        private readonly GameManager _gameManager;
        private bool _isPause;

        public PauseGameController(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void OnPauseResume()
        {
            if (_isPause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        private void ResumeGame()
        {
            _gameManager.ResumeGame();
            _isPause = false;
        }

        private void PauseGame()
        {
            _gameManager.PauseGame();
            _isPause = true;
        }
    }
}