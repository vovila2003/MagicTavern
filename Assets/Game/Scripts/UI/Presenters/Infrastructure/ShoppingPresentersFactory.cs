using JetBrains.Annotations;
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
        private readonly CharacterSeller _characterSeller;
        private readonly IMoneyStorage _moneyStorage;

        public ShoppingPresentersFactory(
            ICommonViewsFactory commonViewsFactory,
            IShoppingViewsFactory shoppingViewsFactory,
            CommonPresentersFactory commonPresentersFactory,
            CharacterSeller characterSeller,
            IMoneyStorage moneyStorage)
        {
            _commonViewsFactory = commonViewsFactory;
            _shoppingViewsFactory = shoppingViewsFactory;
            _commonPresentersFactory = commonPresentersFactory;
            _characterSeller = characterSeller;
            _moneyStorage = moneyStorage;
        }

        public ShoppingPanelPresenter CreateShoppingPanelPresenter() => 
            new(_commonViewsFactory.CreatePanelView(),
            this);

        public CategoriesPresenter CreateCategoriesPresenter(Transform viewContainer) => 
            new(_shoppingViewsFactory.CreateCategoriesView(viewContainer),
                this);

        public FilterPresenter CreateFilterPresenter(Transform viewContainer, string filterText) =>
            new(_shoppingViewsFactory.CreateFilterView(viewContainer), filterText);

        public ItemConfigCardPresenter CreateItemConfigCardPresenter(Transform viewContainer) =>
            new(_commonViewsFactory.GetItemCardView(viewContainer), 
                _commonViewsFactory.ItemCardViewPool);

        public ShopItemsPresenter CreateShopItemsPresenter(Transform viewContainer) =>
            new(_shoppingViewsFactory.CreateShopItemsView(viewContainer), this);

        public VendorInfoPresenter CreateVendorInfoPresenter(Transform viewContainer) =>
            new(_shoppingViewsFactory.CreateVendorInfoView(viewContainer));
        
        public CharacterItemsPresenter CreateCharacterItemsPresenter(Transform viewContainer) =>
            new(_shoppingViewsFactory.CreateCharacterItemsView(viewContainer), 
                _commonPresentersFactory, 
                _characterSeller);

        public CharacterInfoPresenter CreateCharacterInfoPresenter(Transform viewContainer) =>
            new(_shoppingViewsFactory.CreateCharacterInfoView(viewContainer), _moneyStorage);
    }
}