using Modules.GameCycle.Interfaces;
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
        [SerializeField] 
        private MainMenuView MainMenuView;
        
        [SerializeField] 
        private PauseView PauseView;

        [SerializeField] 
        private HudView HudView;

        private PresentersFactory _factory;
        private MainMenuPresenter _mainMenuPresenter;
        private PausePresenter _pausePresenter;
        private HudPresenter _hudPresenter;
        
        [Inject]
        private void Construct(PresentersFactory factory)
        {
            _factory = factory;
        }

        void IInitGameListener.OnInit()
        {
            _mainMenuPresenter = _factory.CreateMainMenuPresenter(MainMenuView);
            _pausePresenter = _factory.CreatePausePresenter(PauseView);
            _hudPresenter = _factory.CreateHudPresenter(HudView);
            
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