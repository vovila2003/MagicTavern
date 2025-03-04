using System;
using Tavern.Gardening;
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
        private Pot _pot;
        private Action _onExit;
        private SeedItemsPresenter _seedItemsPresenter;
        private FertilizerItemsPresenter _fertilizerItemsPresenter;
        private MedicineItemsPresenter _medicineItemsPresenter;
        private PotInfoPresenter _potInfoPresenter;

        public GardeningPanelPresenter(
            IPanelView view,
            Button makeSeedsButton,
            GardeningPresentersFactory gardeningPresentersFactory
            ) : base(view)
        {
            _view = view;
            _makeSeedsButton = makeSeedsButton;
            _gardeningPresentersFactory = gardeningPresentersFactory;
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
    }
}