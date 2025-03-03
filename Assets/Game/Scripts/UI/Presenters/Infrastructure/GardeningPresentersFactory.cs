using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.Gardening;
using Tavern.Gardening.Fertilizer;
using Tavern.Gardening.Medicine;
using Tavern.Settings;
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
        private readonly MedicineInventoryContext _medicineInventoryContext;
        private readonly GameSettings _gameSettings;

        public GardeningPresentersFactory(
            ICommonViewsFactory commonViewsFactory,
            CommonPresentersFactory commonPresentersFactory,
            IGardeningViewsFactory gardeningViewsFactory,
            IInventory<SeedItem> seedInventory,
            FertilizerInventoryContext fertilizerInventoryContext,
            MedicineInventoryContext medicineInventoryContext,
            GameSettings gameSettings
            )
        {
            _commonViewsFactory = commonViewsFactory;
            _commonPresentersFactory = commonPresentersFactory;
            _gardeningViewsFactory = gardeningViewsFactory;
            _seedInventory = seedInventory;
            _fertilizerInventoryContext = fertilizerInventoryContext;
            _medicineInventoryContext = medicineInventoryContext;
            _gameSettings = gameSettings;
        }

        public GardeningPanelPresenter CreateGardeningPanelPresenter()
        {
            IPanelView panelView = _commonViewsFactory.CreatePanelView();
            return new GardeningPanelPresenter(
                panelView,
                _gardeningViewsFactory.CreateMakeSeedsButton(panelView.Container),
                    this);
        }

        public SeedItemsPresenter CreateSeedItemsPresenter(Transform viewContainer) =>
            new(_gardeningViewsFactory.CreateSeedItemsView(viewContainer),
                _commonPresentersFactory,
                _seedInventory);
        
        public FertilizerItemsPresenter CreateFertilizerItemsPresenter(Transform viewContainer) =>
            new(_gardeningViewsFactory.CreateFertilizerItemsView(viewContainer),
                _commonPresentersFactory,
                _fertilizerInventoryContext);
        
        public MedicineItemsPresenter CreateMedicineItemsPresenter(Transform viewContainer) =>
            new(_gardeningViewsFactory.CreateMedicineItemsView(viewContainer),
                _commonPresentersFactory,
                _medicineInventoryContext);

        public PotInfoPresenter CreatePotInfoPresenter(Transform viewContainer) =>
            new(_gardeningViewsFactory.CreatePotInfoView(viewContainer),
                _gameSettings.UISettings.Gardening);

    }
}