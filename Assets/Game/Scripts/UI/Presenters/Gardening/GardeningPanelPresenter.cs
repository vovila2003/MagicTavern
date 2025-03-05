using System;
using Modules.Gardening;
using Modules.Items;
using Tavern.Gardening;
using Tavern.ProductsAndIngredients;
using Tavern.Settings;
using Tavern.Storages;
using UnityEngine;
using UnityEngine.UI;

namespace Tavern.UI.Presenters
{
    public class GardeningPanelPresenter : BasePresenter
    {
        private const string Title = "Садоводство";
        
        private readonly IPanelView _view;
        private readonly Button _makeSeedsButton;
        private readonly GardeningPresentersFactory _gardeningPresentersFactory;
        private readonly Func<Transform, InfoPresenter> _infoPresenterFactory;
        private readonly Transform _canvas;
        private readonly PlantProductCatalog _catalog;
        private readonly SlopsItemConfig _slopsConfig;

        private Pot _pot;
        private Action _onExit;
        private SeedItemsPresenter _seedItemsPresenter;
        private FertilizerItemsPresenter _fertilizerItemsPresenter;
        private MedicineItemsPresenter _medicineItemsPresenter;
        private PotInfoPresenter _potInfoPresenter;
        private InfoPresenter _infoPresenter;

        public GardeningPanelPresenter(
            IPanelView view,
            Button makeSeedsButton,
            GardeningPresentersFactory gardeningPresentersFactory,
            Func<Transform, InfoPresenter> infoPresenterFactory,
            Transform canvas,
            GameSettings gameSettings
            ) : base(view)
        {
            _view = view;
            _makeSeedsButton = makeSeedsButton;
            _gardeningPresentersFactory = gardeningPresentersFactory;
            _infoPresenterFactory = infoPresenterFactory;
            _canvas = canvas;
            _catalog = gameSettings.GardeningSettings.PlantProductCatalog;
            _slopsConfig = gameSettings.UISettings.CommonSettings.SlopsSettings;
        }

        public void Show(Pot pot, Action onExit)
        {
            _pot = pot;
            _onExit = onExit;
            Show();
        }

        protected override void OnShow()
        {
            SetupView();
            SetupSeeds();
            SetupFertilizer();
            SetupMedicine();
            SetupPotInfo();

            _pot.OnHarvestReceived += OnHarvestReceived;
            _pot.OnSlopsReceived += OnSlopsReceived;
        }

        protected override void OnHide()
        {
            _view.OnCloseClicked -= Hide;
            _makeSeedsButton.onClick.AddListener(OnMakeSeedsClicked);
            
            _seedItemsPresenter.Hide();
            _seedItemsPresenter.OnSeeded -= OnSeeded;
            
            _fertilizerItemsPresenter.Hide();
            _fertilizerItemsPresenter.OnFertilized -= OnFertilized;
            
            _medicineItemsPresenter.Hide();
            _medicineItemsPresenter.OnHeal -= OnHeal;
            
            _potInfoPresenter.Hide();
            _potInfoPresenter.OnGather -= OnGather;
            _potInfoPresenter.OnWatering -= OnWatering;
            
            _pot.OnHarvestReceived -= OnHarvestReceived;
            _pot.OnSlopsReceived -= OnSlopsReceived;
            
            _onExit?.Invoke();
        }

        private void SetupView()
        {
            _view.SetTitle(Title);
            _view.OnCloseClicked += Hide;
            _makeSeedsButton.onClick.AddListener(OnMakeSeedsClicked);
        }

        private void SetupSeeds()
        {
            _seedItemsPresenter ??= _gardeningPresentersFactory.CreateSeedItemsPresenter(_view.Container);
            _seedItemsPresenter.OnSeeded += OnSeeded;
            _seedItemsPresenter.Show(_pot);
        }

        private void SetupFertilizer()
        {
            _fertilizerItemsPresenter ??= _gardeningPresentersFactory.CreateFertilizerItemsPresenter(_view.Container);
            _fertilizerItemsPresenter.OnFertilized += OnFertilized;
            _fertilizerItemsPresenter.Show(_pot);
        }

        private void SetupMedicine()
        {
            _medicineItemsPresenter ??= _gardeningPresentersFactory.CreateMedicineItemsPresenter(_view.Container);
            _medicineItemsPresenter.OnHeal += OnHeal;
            _medicineItemsPresenter.Show(_pot);
        }

        private void SetupPotInfo()
        {
            _potInfoPresenter ??= _gardeningPresentersFactory.CreatePotInfoPresenter(_view.Container);
            _potInfoPresenter.OnGather += OnGather;
            _potInfoPresenter.OnWatering += OnWatering;
            _potInfoPresenter.Show(_pot);
        }

        private void OnMakeSeedsClicked()
        {
            //TODO
            Debug.Log("MakeSeeds clicked");
        }

        private void OnSeeded(bool result)
        {
            UpdateInfo();
            if (!result) return;
            
            _seedItemsPresenter.SetActive(false);
            _fertilizerItemsPresenter.SetActive(true);
            _medicineItemsPresenter.SetActive(true);
        }

        private void OnFertilized(bool result)
        {
            UpdateInfo();
            if (!result) return;
            
            _fertilizerItemsPresenter.SetActive(false);
        }

        private void OnHeal(bool result)
        {
            OnFertilized(result);
        }

        private void OnGather()
        {
            UpdateInfo();
            _seedItemsPresenter.SetActive(true);
            _fertilizerItemsPresenter.SetActive(false);
            _medicineItemsPresenter.SetActive(false);
        }

        private void OnWatering()
        {
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            _potInfoPresenter.Hide();
            _potInfoPresenter.Show(_pot);
        }

        private void OnSlopsReceived(int count)
        {
            _infoPresenter ??= _infoPresenterFactory(_canvas);
            var additionDescription = $"Количество: {count}";

            _infoPresenter.Show(_slopsConfig.Create(), InfoPresenter.Mode.Info,
                string.Empty, additionDescription);
        }

        private void OnHarvestReceived(PlantConfig config, int count, bool hasSeed)
        {
            string name = PlantProductNameProvider.GetName(config.Name);
            if (!_catalog.TryGetItem(name, out ItemConfig itemConfig))
            {
                Debug.Log($"{name} is not fount in catalog");
                return;
            }

            if (itemConfig is not PlantItemConfig plantConfig)
            {
                return;
            }

            var additionDescription = $"Количество: {count}";
            if (hasSeed)
            {
                additionDescription = $"{additionDescription}\nДополнительная семечка!";
            }
            
            _infoPresenter ??= _infoPresenterFactory(_canvas);
            _infoPresenter.Show(plantConfig.Create(), InfoPresenter.Mode.Info, 
                string.Empty, additionDescription);
        }
    }
}