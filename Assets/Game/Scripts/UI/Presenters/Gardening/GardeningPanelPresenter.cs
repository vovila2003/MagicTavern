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
        private Pot _pot;
        private Action _onExit;
        private readonly SeedItemsPresenter _seedItemsPresenter;
        private readonly FertilizerItemsPresenter _fertilizerItemsPresenter;
        private readonly MedicineItemsPresenter _medicineItemsPresenter;
        private readonly Button _createSeedsButton;

        public GardeningPanelPresenter(
            IPanelView view,
            Button makeSeedsButton,
            GardeningPresentersFactory gardeningPresentersFactory
            ) : base(view)
        {
            _view = view;
            _makeSeedsButton = makeSeedsButton;
            _seedItemsPresenter = gardeningPresentersFactory.CreateSeedItemsPresenter(_view.Container);
            _fertilizerItemsPresenter = gardeningPresentersFactory.CreateFertilizerItemsPresenter(_view.Container);
            _medicineItemsPresenter = gardeningPresentersFactory.CreateMedicineItemsPresenter(_view.Container);
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
        }

        protected override void OnHide()
        {
            _onExit?.Invoke();
            
            _view.OnCloseClicked -= Hide;
            _makeSeedsButton.onClick.AddListener(OnMakeSeedsClicked);
        }

        private void SetupView()
        {
            _view.SetTitle(Title);
            _view.OnCloseClicked += Hide;
            _makeSeedsButton.onClick.AddListener(OnMakeSeedsClicked);
        }

        private void SetupSeeds()
        {
            _seedItemsPresenter.Show();
        }

        private void SetupFertilizer()
        {
            _fertilizerItemsPresenter.Show();
        }

        private void SetupMedicine()
        {
            _medicineItemsPresenter.Show();
        }

        private void OnMakeSeedsClicked()
        {
            Debug.Log("MakeSeeds clicked");
        }
    }
}