using Modules.Items;
using Tavern.Shopping;

namespace Tavern.UI.Presenters
{
    public class VendorInfoPresenter : BasePresenter
    {
        private readonly IVendorInfoView _view;
        private NpcSeller _seller;

        public VendorInfoPresenter(IVendorInfoView view) : base(view)
        {
            _view = view;
        }

        public void Show(NpcSeller seller)
        {
            _seller = seller;
            Show();
        }

        protected override void OnShow()
        {
            Metadata metadata = _seller.Config.Metadata;
            _view.SetTitle(metadata.Title);
            _view.SetIcon(metadata.Icon);
            _view.SetMaxStars(ShoppingConfig.MaxReputation);
            OnReputationChanged(_seller.CurrentReputation);
            OnMoneyChanged(_seller.Money);

            _seller.OnReputationChanged += OnReputationChanged;
            _seller.OnSellerMoneyChanged += OnMoneyChanged;
        }

        protected override void OnHide()
        {
            _seller.OnReputationChanged -= OnReputationChanged;
            _seller.OnSellerMoneyChanged -= OnMoneyChanged;
        }

        private void OnReputationChanged(int value) => _view.SetStars(value);

        private void OnMoneyChanged(int value) => _view.SetMoney($"{value} р");
    }
}