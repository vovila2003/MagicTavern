using Tavern.Storages.CurrencyStorages;

namespace Tavern.UI.Presenters
{
    public class CharacterInfoPresenter : BasePresenter
    {
        private readonly ICharacterInfoView _view;
        private readonly IMoneyStorage _moneyStorage;

        public CharacterInfoPresenter(
            ICharacterInfoView view,
            IMoneyStorage moneyStorage
            ) : base(view)
        {
            _view = view;
            _moneyStorage = moneyStorage;
        }

        protected override void OnShow()
        {
            OnMoneyChanged(_moneyStorage.Money);

            _moneyStorage.OnMoneyChanged += OnMoneyChanged;
        }

        protected override void OnHide()
        {
            _moneyStorage.OnMoneyChanged -= OnMoneyChanged;
        }

        private void OnMoneyChanged(int value) => _view.SetMoney($"{value} р");
    }
}