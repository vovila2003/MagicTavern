using JetBrains.Annotations;
using Tavern.Infrastructure;
using Tavern.InputServices.Interfaces;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    [UsedImplicitly]
    public class CommonPresentersFactory
    {
        private readonly GameCycleController _gameCycleController;
        private readonly ICommonViewsFactory _commonViewsFactory;
        private readonly IMouseClickInput _mouseClickInput;

        public CommonPresentersFactory(
            GameCycleController gameCycleController, 
            ICommonViewsFactory commonViewsFactory,
            IMouseClickInput mouseClickInput)
        {
            _gameCycleController = gameCycleController;
            _commonViewsFactory = commonViewsFactory;
            _mouseClickInput = mouseClickInput;
        }

        public MainMenuPresenter CreateMainMenuPresenter(IMainMenuView mainMenuView, UiManager uiManager) => 
            new(mainMenuView, _gameCycleController, uiManager);

        public HudPresenter CreateHudPresenter(IHudView hudView) => new(hudView);

        public PausePresenter CreatePausePresenter(IPauseView pauseView, UiManager uiManager) => 
            new(pauseView, _gameCycleController, uiManager);
        
        public ItemCardPresenter CreateItemCardPresenter(Transform viewContentTransform) =>
            new(_commonViewsFactory.GetItemCardView(viewContentTransform), 
                _commonViewsFactory.ItemCardViewPool);
        
        public AutoClosePresenter CreateAutoClosePresenter() => new(_mouseClickInput);
        
        public InfoPresenter CreateInfoPresenter(Transform parent) =>
            new(_commonViewsFactory.InfoViewProvider, parent, this);
    }
}