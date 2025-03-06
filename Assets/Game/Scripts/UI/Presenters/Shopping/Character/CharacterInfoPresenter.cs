using Tavern.Character;
using Tavern.Cooking;
using Tavern.Effects;
using Tavern.Storages.CurrencyStorages;

namespace Tavern.UI.Presenters
{
    public class CharacterInfoPresenter : BasePresenter
    {
        private readonly ICharacterInfoView _view;
        private readonly IMoneyStorage _moneyStorage;
        private readonly ICommonViewsFactory _viewsFactory;
        private readonly CharacterState _characterState;

        public CharacterInfoPresenter(
            ICharacterInfoView view,
            IMoneyStorage moneyStorage, 
            ICharacter character,
            ICommonViewsFactory viewsFactory
            ) : base(view)
        {
            _view = view;
            _moneyStorage = moneyStorage;
            _viewsFactory = viewsFactory;
            _characterState = character.GetState();
        }

        protected override void OnShow()
        {
            _view.SetIcon(_characterState.Metadata.Icon);
            OnMoneyChanged(_moneyStorage.Money);
            AddEffects();

            _moneyStorage.OnMoneyChanged += OnMoneyChanged;
            _characterState.OnEffectsChanged += OnEffectChanged;
        }

        protected override void OnHide()
        {
            _moneyStorage.OnMoneyChanged -= OnMoneyChanged;
            _characterState.OnEffectsChanged -= OnEffectChanged;
        }

        private void OnMoneyChanged(int value) => _view.SetMoney($"{value} р");

        private void OnEffectChanged()
        {
            ClearAllEffects();
            AddEffects();
        }

        private void AddEffects()
        {
            foreach (EffectConfig effectConfig in _characterState.Effects)
            {
                IEffectView effectView = _viewsFactory.CreateEffectView(_view.Content);
                effectView.SetActive(true);
                effectView.SetIcon(effectConfig.Icon);
                _view.AddEffect(effectView);
            }
        }

        private void ClearAllEffects()
        {
            _view.ClearEffects();
        }
    }
}