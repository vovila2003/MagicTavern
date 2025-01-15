using Modules.GameCycle.Interfaces;
using Tavern.Settings;
using Tavern.UI.Presenters;
using Tavern.UI.Views;
using UnityEngine;
using VContainer;

namespace Tavern.UI
{
    public sealed class UiManager : MonoBehaviour, 
        IInitGameListener,
        IPrepareGameListener,
        IStartGameListener,
        IPauseGameListener,
        IResumeGameListener,
        IFinishGameListener
    {
        private UITransformSettings _settings;
        private PresentersFactory _factory;
        private MainMenuPresenter _mainMenuPresenter;
        private PausePresenter _pausePresenter;
        private HudPresenter _hudPresenter;
        
        [Inject]
        private void Construct(PresentersFactory factory, UITransformSettings settings)
        {
            _factory = factory;
            _settings = settings;
        }

        void IInitGameListener.OnInit()
        {
            _mainMenuPresenter = _factory.CreateMainMenuPresenter(_settings.MainMenu);
            _pausePresenter = _factory.CreatePausePresenter(_settings.Pause);
            _hudPresenter = _factory.CreateHudPresenter(_settings.Hud);
            
            _mainMenuPresenter.Show();
        }

        void IPrepareGameListener.OnPrepare()
        {
            _mainMenuPresenter.Hide();
        }

        void IStartGameListener.OnStart()
        {
            _hudPresenter.Show();
        }

        void IPauseGameListener.OnPause()
        {
            _hudPresenter.Hide();
            _pausePresenter.Show();            
        }

        void IResumeGameListener.OnResume()
        {
            _pausePresenter.Hide();
            _hudPresenter.Show();
        }

        void IFinishGameListener.OnFinish()
        {
            _hudPresenter.Hide();
            _mainMenuPresenter.Show();
        }
    }
}