using System;
using Tavern.Gardening;

namespace Tavern.UI.Presenters
{
    public class GardeningPanelPresenter : BasePresenter
    {
        private const string Title = "Садоводство";
        
        private readonly IPanelView _view;
        private Pot _pot;
        private Action _onExit;
        private readonly SeedItemsPresenter _seedItemsPresenter;
        private readonly FertilizerItemsPresenter _fertilizerItemsPresenter;
        private readonly MedicineItemsPresenter _medicineItemsPresenter;

        public GardeningPanelPresenter(
            IPanelView view,
            GardeningPresentersFactory gardeningPresentersFactory
            ) : base(view)
        {
            _view = view;
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
        }

        private void SetupView()
        {
            _view.SetTitle(Title);
            _view.OnCloseClicked += Hide;
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
    }
}