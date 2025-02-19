using Tavern.UI.Views;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface ICharacterInfoView : IView
    {
        void SetIcon(Sprite sprite);
        void AddEffect(EffectView effect);
        void ClearEffects();
        void SetMoney(string text);
    }
}