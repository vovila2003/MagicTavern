using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface ICharacterInfoView : IView
    {
        void SetIcon(Sprite sprite);
        void AddEffect(IEffectView effect);
        void ClearEffects();
        void SetMoney(string text);
        Transform Content { get; }
    }
}