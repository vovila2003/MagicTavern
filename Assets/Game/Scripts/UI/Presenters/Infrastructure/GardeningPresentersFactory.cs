using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.Gardening;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    [UsedImplicitly]
    public class GardeningPresentersFactory
    {
        private readonly ICommonViewsFactory _commonViewsFactory;
        private readonly CommonPresentersFactory _commonPresentersFactory;
        private readonly IGardeningViewsFactory _gardeningViewsFactory;
        private readonly IInventory<SeedItem> _seedInventory;

        public GardeningPresentersFactory(
            ICommonViewsFactory commonViewsFactory,
            CommonPresentersFactory commonPresentersFactory,
            IGardeningViewsFactory gardeningViewsFactory,
            IInventory<SeedItem> seedInventory)
        {
            _commonViewsFactory = commonViewsFactory;
            _commonPresentersFactory = commonPresentersFactory;
            _gardeningViewsFactory = gardeningViewsFactory;
            _seedInventory = seedInventory;
        }

        public GardeningPanelPresenter CreateGardeningPanelPresenter() =>
            new(_commonViewsFactory.CreatePanelView(), 
                this);

        public SeedItemsPresenter CreateSeedItemsPresenter(Transform viewContainer) =>
            new(_gardeningViewsFactory.CreateSeedItemsView(viewContainer),
                _commonPresentersFactory,
                _seedInventory);
    }
}