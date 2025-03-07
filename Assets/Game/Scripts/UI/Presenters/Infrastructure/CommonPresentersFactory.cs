using JetBrains.Annotations;
using Modules.GameCycle;
using Tavern.InputServices.Interfaces;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    [UsedImplicitly]
    public class CommonPresentersFactory
    {
        private readonly GameCycle _gameCycle;
        private readonly ICommonViewsFactory _commonViewsFactory;
        private readonly IMouseClickInput _mouseClickInput;

        public CommonPresentersFactory(
            GameCycle gameCycle, 
            ICommonViewsFactory commonViewsFactory,
            IMouseClickInput mouseClickInput)
        {
            _gameCycle = gameCycle;
            _commonViewsFactory = commonViewsFactory;
            _mouseClickInput = mouseClickInput;
        }

        public MainMenuPresenter CreateMainMenuPresenter(IMainMenuView mainMenuView, IUiManager uiManager) => 
            new(mainMenuView, _gameCycle, uiManager);

        public HudPresenter CreateHudPresenter(IHudView hudView) => new(hudView);

        public PausePresenter CreatePausePresenter(IPauseView pauseView, IUiManager uiManager) => 
            new(pauseView, _gameCycle, uiManager);
        
        public ItemCardPresenter CreateItemCardPresenter(Transform viewContentTransform) =>
            new(_commonViewsFactory.GetItemCardView(viewContentTransform), 
                _commonViewsFactory.ItemCardViewPool);
        
        public AutoClosePresenter CreateAutoClosePresenter() => new(_mouseClickInput);
        
        public InfoPresenter CreateInfoPresenter(Transform parent) =>
            new(_commonViewsFactory.InfoViewProvider, parent, this);
    }
}