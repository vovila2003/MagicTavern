using System;
using Modules.GameCycle.Interfaces;
using Tavern.Cooking;
using Tavern.Gardening;
using Tavern.Settings;
using Tavern.Shopping;
using Tavern.UI.Presenters;
using UnityEngine;
using VContainer;

namespace Tavern.UI
{
    public sealed class UiManager : MonoBehaviour, 
        IInitGameListener, IUiManager
    {
        private UISceneSettings _settings;
        private CommonPresentersFactory _commonFactory;
        private CookingPresentersFactory _cookingFactory;
        private ShoppingPresentersFactory _shoppingFactory;
        private GardeningPresentersFactory _gardeningFactory;
        
        private MainMenuPresenter _mainMenuPresenter;
        private PausePresenter _pausePresenter;
        private HudPresenter _hudPresenter;
        private CookingPanelPresenter _cookingPresenter;
        private ShoppingPanelPresenter _shoppingPresenter;
        private GardeningPanelPresenter _gardeningPresenter;
        private BasePresenter _currentPresenter;
        
        public bool IsOpen => _currentPresenter != null;

        [Inject]
        private void Construct(
            CommonPresentersFactory commonFactory,
            CookingPresentersFactory cookingFactory,
            ShoppingPresentersFactory shoppingFactory,
            GardeningPresentersFactory gardeningFactory,
            SceneSettings settings)
        {
            _commonFactory = commonFactory;
            _cookingFactory = cookingFactory;
            _shoppingFactory = shoppingFactory;
            _gardeningFactory = gardeningFactory;
            _settings = settings.UISceneSettings;
        }

        public void ShowCookingUi(KitchenItemConfig kitchenItemConfig, Action onExit)
        {
            _cookingPresenter ??= _cookingFactory.CreateCookingPanelPresenter();
            
            _cookingPresenter.Show(kitchenItemConfig, () =>
            {
                onExit?.Invoke();
                _currentPresenter = null;
            });
            _currentPresenter = _cookingPresenter;
        }
        
        public void ShowShoppingUi(Shop shop, Action onExit)
        {
            _shoppingPresenter ??= _shoppingFactory.CreateShoppingPanelPresenter();
            
            _shoppingPresenter.Show(shop, () =>
            {
                onExit?.Invoke();
                _currentPresenter = null;
            });
            _currentPresenter = _shoppingPresenter;
        }
        
        public void ShowGardeningUi(Pot pot, Action onExit)
        {
            _gardeningPresenter ??= _gardeningFactory.CreateGardeningPanelPresenter();
            
            _gardeningPresenter.Show(pot, () =>
            {
                onExit?.Invoke();
                _currentPresenter = null;
            });
            _currentPresenter = _gardeningPresenter;
        }

        public void HideUi()
        {
            _currentPresenter?.Hide();
            _currentPresenter = null;
        }

        public void ShowMainMenu()
        {
            _hudPresenter.Hide();
            _mainMenuPresenter.Show();
        }

        public void ShowHud()
        {
            _mainMenuPresenter.Hide();
            _hudPresenter.Show();
        }

        public void HideHud()
        {
            _hudPresenter.Hide();
            _mainMenuPresenter.Show();
        }

        public void ShowPause()
        {
            _pausePresenter.Show();
        }

        public void HidePause()
        {
            _pausePresenter.Hide();
        }
        
        void IInitGameListener.OnInit()
        {
            _mainMenuPresenter = _commonFactory.CreateMainMenuPresenter(_settings.MainMenu, this);
            _pausePresenter = _commonFactory.CreatePausePresenter(_settings.Pause, this);
            _hudPresenter = _commonFactory.CreateHudPresenter(_settings.Hud);
            
            _mainMenuPresenter.Show();
        }
    }
}