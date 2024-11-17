using Modules.GameCycle.Interfaces;
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

        private IViewModelFactory _factory;

        [Inject]
        private void Construct(IViewModelFactory factory)
        {
            _factory = factory;
        }

        void IInitGameListener.OnInit()
        {
            MainMenuView.Show(_factory.CreateMainMenuViewModel());
        }

        void IPrepareGameListener.OnPrepare()
        {
            MainMenuView.Hide();
        }

        void IStartGameListener.OnStart()
        {
            HudView.Show(_factory.CreateHudViewModel());
        }

        void IPauseGameListener.OnPause()
        {
            HudView.Hide();
            PauseView.Show(_factory.CreatePauseViewModel());
        }

        void IResumeGameListener.OnResume()
        {
            PauseView.Hide();
            HudView.Show(_factory.CreateHudViewModel());
        }

        void IFinishGameListener.OnFinish()
        {
            HudView.Hide();
            MainMenuView.Show(_factory.CreateMainMenuViewModel());
        }
    }
}