using System.Collections.Generic;
using Tavern.UI.Presenters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public class CharacterInfoView : View, ICharacterInfoView
    {
        [SerializeField] 
        private Image Icon;
        
        [SerializeField] 
        private Transform EffectContent;

        [SerializeField]
        private TMP_Text Money;
        
        public Transform Content => EffectContent;
        
        private readonly List<IEffectView> _effects = new();

        public void SetIcon(Sprite sprite)
        {
            Icon.sprite = sprite;
        }
        
        public void AddEffect(IEffectView effect)
        {
            _effects.Add(effect);
        }

        public void ClearEffects()
        {
            foreach (IEffectView effect in _effects)
            {
                Destroy(effect.GetGameObject());
            }
            
            _effects.Clear();
        }

        public void SetMoney(string text)
        {
            Money.text = text;
        }
    }
}