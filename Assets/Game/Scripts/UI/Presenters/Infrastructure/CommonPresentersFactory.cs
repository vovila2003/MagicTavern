using JetBrains.Annotations;
using Modules.GameCycle;
using Modules.SaveLoad;
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
        private readonly GameSaveLoader _saveLoader;

        public CommonPresentersFactory(
            GameCycle gameCycle, 
            ICommonViewsFactory commonViewsFactory,
            IMouseClickInput mouseClickInput,
            GameSaveLoader saveLoader)
        {
            _gameCycle = gameCycle;
            _commonViewsFactory = commonViewsFactory;
            _mouseClickInput = mouseClickInput;
            _saveLoader = saveLoader;
        }

        public MainMenuPresenter CreateMainMenuPresenter(IMainMenuView mainMenuView, IUiManager uiManager) => 
            new(mainMenuView, _gameCycle, uiManager);

        public HudPresenter CreateHudPresenter(IHudView hudView) => new(hudView);

        public PausePresenter CreatePausePresenter(IPauseView pauseView, IUiManager uiManager) => 
            new(pauseView,
                _gameCycle,
                uiManager,
                _saveLoader);
        
        public ItemCardPresenter CreateItemCardPresenter(Transform viewContentTransform) =>
            new(_commonViewsFactory.GetItemCardView(viewContentTransform), 
                _commonViewsFactory.ItemCardViewPool);
        
        public AutoClosePresenter CreateAutoClosePresenter() => new(_mouseClickInput);
        
        public InfoPresenter CreateInfoPresenter(Transform parent) =>
            new(_commonViewsFactory.InfoViewProvider, parent, this);
    }
}