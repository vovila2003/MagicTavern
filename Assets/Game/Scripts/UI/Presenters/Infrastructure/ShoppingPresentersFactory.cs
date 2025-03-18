using JetBrains.Annotations;
using Tavern.Character;
using Tavern.Settings;
using Tavern.Shopping;
using Tavern.Storages.CurrencyStorages;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    [UsedImplicitly]
    public class ShoppingPresentersFactory
    {
        private readonly ICommonViewsFactory _commonViewsFactory;
        private readonly IShoppingViewsFactory _shoppingViewsFactory;
        private readonly CommonPresentersFactory _commonPresentersFactory;
        private readonly ICharacter _character;
        private readonly CharacterSeller _characterSeller;
        private readonly IMoneyStorage _moneyStorage;
        private readonly UISceneSettings _uiSceneSettings;

        public ShoppingPresentersFactory(
            ICommonViewsFactory commonViewsFactory,
            IShoppingViewsFactory shoppingViewsFactory,
            CommonPresentersFactory commonPresentersFactory,
            ICharacter character, 
            CharacterSeller characterSeller,
            IMoneyStorage moneyStorage, 
            SceneSettings sceneSettings)
        {
            _commonViewsFactory = commonViewsFactory;
            _shoppingViewsFactory = shoppingViewsFactory;
            _commonPresentersFactory = commonPresentersFactory;
            _character = character;
            _characterSeller = characterSeller;
            _moneyStorage = moneyStorage;
            _uiSceneSettings = sceneSettings.UISceneSettings;
        }

        public ShoppingPanelPresenter CreateShoppingPanelPresenter() => 
            new(_commonViewsFactory.CreatePanelView(),
            this);

        public CategoriesPresenter CreateCategoriesPresenter(Transform viewContainer) => 
            new(_shoppingViewsFactory.CreateCategoriesView(viewContainer),
                this);

        public FilterPresenter CreateFilterPresenter(Transform viewContainer, string filterText) =>
            new(_shoppingViewsFactory.CreateFilterView(viewContainer), 
                filterText);

        public ItemConfigCardPresenter CreateItemConfigCardPresenter(Transform viewContainer) =>
            new(_commonViewsFactory.GetItemCardView(viewContainer), 
                _commonViewsFactory.ItemCardViewPool);

        public ShopItemsPresenter CreateShopItemsPresenter(Transform viewContainer) =>
            new(_shoppingViewsFactory.CreateShopItemsView(viewContainer), 
                this,
                _uiSceneSettings.Canvas);

        public VendorInfoPresenter CreateVendorInfoPresenter(Transform viewContainer) =>
            new(_shoppingViewsFactory.CreateVendorInfoView(viewContainer));
        
        public CharacterItemsPresenter CreateCharacterItemsPresenter(Transform viewContainer) =>
            new(_shoppingViewsFactory.CreateCharacterItemsView(viewContainer), 
                _commonPresentersFactory, 
                this,
                _characterSeller,
                _uiSceneSettings.Canvas);

        public CharacterInfoPresenter CreateCharacterInfoPresenter(Transform viewContainer) =>
            new(_shoppingViewsFactory.CreateCharacterInfoView(viewContainer), 
                _moneyStorage,
                _character,
                _commonViewsFactory);
        
        public DealInfoPresenter CreateDealInfoPresenter(Transform parent) =>
            new(_shoppingViewsFactory.DealInfoViewProvider, 
                parent,
                _commonPresentersFactory);

        public ShopCharacterItemsPresenter CreateShopCharacterItemsPresenter(Transform viewContainer) =>
            new(_shoppingViewsFactory.CreateShopItemsView(viewContainer), 
                this,
                _commonPresentersFactory,
                _uiSceneSettings.Canvas);
    }
}