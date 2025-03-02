using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.Gardening;
using Tavern.Gardening.Fertilizer;
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
        private readonly FertilizerInventoryContext _fertilizerInventoryContext;

        public GardeningPresentersFactory(
            ICommonViewsFactory commonViewsFactory,
            CommonPresentersFactory commonPresentersFactory,
            IGardeningViewsFactory gardeningViewsFactory,
            IInventory<SeedItem> seedInventory,
            FertilizerInventoryContext fertilizerInventoryContext)
        {
            _commonViewsFactory = commonViewsFactory;
            _commonPresentersFactory = commonPresentersFactory;
            _gardeningViewsFactory = gardeningViewsFactory;
            _seedInventory = seedInventory;
            _fertilizerInventoryContext = fertilizerInventoryContext;
        }

        public GardeningPanelPresenter CreateGardeningPanelPresenter() =>
            new(_commonViewsFactory.CreatePanelView(), 
                this);

        public SeedItemsPresenter CreateSeedItemsPresenter(Transform viewContainer) =>
            new(_gardeningViewsFactory.CreateSeedItemsView(viewContainer),
                _commonPresentersFactory,
                _seedInventory);
        
        public FertilizerItemsPresenter CreateFertilizerItemsPresenter(Transform viewContainer) =>
            new(_gardeningViewsFactory.CreateFertilizerItemsView(viewContainer),
                _commonPresentersFactory,
                _fertilizerInventoryContext);
    }
}