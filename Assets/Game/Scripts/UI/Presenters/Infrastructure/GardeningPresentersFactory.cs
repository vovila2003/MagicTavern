using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.Gardening;
using Tavern.Gardening.Fertilizer;
using Tavern.Gardening.Medicine;
using Tavern.ProductsAndIngredients;
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
        private readonly IInventory<PlantProductItem> _productInventory;
        private readonly FertilizerInventoryContext _fertilizerInventoryContext;
        private readonly MedicineInventoryContext _medicineInventoryContext;
        private readonly GameSettings _gameSettings;
        private readonly SceneSettings _sceneSettings;
        private readonly Seeder _seeder;
        private readonly SeedMaker _seedMaker;

        public GardeningPresentersFactory(
            ICommonViewsFactory commonViewsFactory,
            CommonPresentersFactory commonPresentersFactory,
            IGardeningViewsFactory gardeningViewsFactory,
            IInventory<SeedItem> seedInventory,
            IInventory<PlantProductItem> productInventory,
            FertilizerInventoryContext fertilizerInventoryContext,
            MedicineInventoryContext medicineInventoryContext,
            GameSettings gameSettings,
            SceneSettings sceneSettings,
            Seeder seeder,
            SeedMaker seedMaker
            )
        {
            _commonViewsFactory = commonViewsFactory;
            _commonPresentersFactory = commonPresentersFactory;
            _gardeningViewsFactory = gardeningViewsFactory;
            _seedInventory = seedInventory;
            _productInventory = productInventory;
            _fertilizerInventoryContext = fertilizerInventoryContext;
            _medicineInventoryContext = medicineInventoryContext;
            _gameSettings = gameSettings;
            _sceneSettings = sceneSettings;
            _seeder = seeder;
            _seedMaker = seedMaker;
        }

        public GardeningPanelPresenter CreateGardeningPanelPresenter()
        {
            IPanelView panelView = _commonViewsFactory.CreatePanelView();
            return new GardeningPanelPresenter(
                panelView,
                _gardeningViewsFactory.CreateMakeSeedsButton(panelView.Container),
                this,
                _commonPresentersFactory.CreateInfoPresenter,
                _sceneSettings.UISceneSettings.Canvas,
                _gameSettings);
        }

        public SeedItemsPresenter CreateSeedItemsPresenter(Transform viewContainer) =>
            new(_gardeningViewsFactory.CreateSeedItemsView(viewContainer),
                _commonPresentersFactory,
                _seedInventory,
                _seeder,
                _commonPresentersFactory.CreateInfoPresenter,
                _sceneSettings.UISceneSettings.Canvas);
        
        public FertilizerItemsPresenter CreateFertilizerItemsPresenter(Transform viewContainer) =>
            new(_gardeningViewsFactory.CreateFertilizerItemsView(viewContainer),
                _commonPresentersFactory,
                _fertilizerInventoryContext,
                _seeder,
                _commonPresentersFactory.CreateInfoPresenter,
                _sceneSettings.UISceneSettings.Canvas);
        
        public MedicineItemsPresenter CreateMedicineItemsPresenter(Transform viewContainer) =>
            new(_gardeningViewsFactory.CreateMedicineItemsView(viewContainer),
                _commonPresentersFactory,
                _medicineInventoryContext,
                _seeder,
                _commonPresentersFactory.CreateInfoPresenter,
                _sceneSettings.UISceneSettings.Canvas);

        public PotInfoPresenter CreatePotInfoPresenter(Transform viewContainer) =>
            new(_gardeningViewsFactory.CreatePotInfoView(viewContainer),
                _gameSettings.UISettings.Gardening,
                _seeder);

        public SeedMakerPresenter CreateSeedMakerPresenter() => 
            new(_commonViewsFactory.CreateSmallPanelView(),
                this);

        public SeedMakerProductItemsPresenter CreateSeedMakerProductItemsPresenter(Transform viewContainer) =>
            new(_gardeningViewsFactory.CreateSeedMakerProductItemsView(viewContainer),
                _commonPresentersFactory,
                this,
                _productInventory,
                _seedMaker,
                _sceneSettings.UISceneSettings.Canvas);
        
        public SeedMakerSeedsPresenter CreateSeedMakerSeedsPresenter(Transform viewContainer) =>
            new(_gardeningViewsFactory.CreateSeedMakerSeedsView(viewContainer),
                _commonPresentersFactory,
                _seedInventory,
                _commonPresentersFactory.CreateInfoPresenter,
                _sceneSettings.UISceneSettings.Canvas);
        
        public ConvertInfoPresenter CreateConvertInfoPresenter(Transform parent) =>
            new(_gardeningViewsFactory.ConvertInfoViewProvider, 
                parent,
                _commonPresentersFactory);
    }
}