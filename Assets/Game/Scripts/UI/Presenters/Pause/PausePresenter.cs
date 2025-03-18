using Modules.GameCycle;
using Modules.SaveLoad;

namespace Tavern.UI.Presenters
{
    public sealed class PausePresenter : BasePresenter
    {
        private readonly IPauseView _view;
        private readonly GameCycle _gameCycle;
        private readonly IUiManager _uiManager;
        private readonly GameSaveLoader _saveLoader;

        public PausePresenter(
            IPauseView view, 
            GameCycle gameCycle, 
            IUiManager uiManager,
            GameSaveLoader saveLoader
            ) : base(view)
        {
            _view = view;
            _gameCycle = gameCycle;
            _uiManager = uiManager;
            _saveLoader = saveLoader;
        }

        protected override void OnShow()
        {
            _view.OnResume += OnResume;
            _view.OnExit += OnExit;
            _view.OnSave += OnSave;
            _view.OnLoad += OnLoad;
        }

        protected override void OnHide()
        {
            _view.OnResume -= OnResume;
            _view.OnExit -= OnExit;
            _view.OnSave -= OnSave;
            _view.OnLoad -= OnLoad;
        }

        private void OnResume()
        {
            _uiManager.HidePause();
            _gameCycle.ResumeGame();            
        }

        private void OnExit()
        {
            _gameCycle.ExitGame();
        }

        private void OnSave()
        {
            _saveLoader.Save();
        }

        private void OnLoad()
        {
            _saveLoader.Load();
        }
    }
}