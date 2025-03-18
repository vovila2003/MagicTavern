using System;
using Tavern.Cooking;
using Tavern.Gardening;
using Tavern.Shopping;

namespace Tavern.UI
{
    public interface IUiManager
    {
        bool IsOpen { get; }
        void ShowCookingUi(KitchenItemConfig kitchenItemConfig, Action onExit);
        void ShowShoppingUi(Shop shop, Action onExit);
        void ShowGardeningUi(Pot pot, Action onExit);
        void HideUi();
        void ShowMainMenu();
        void ShowHud();
        void HideHud();
        void ShowPause();
        void HidePause();
    }
}