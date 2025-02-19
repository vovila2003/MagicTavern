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
        
        
        private readonly List<EffectView> _effects = new();

        public void SetIcon(Sprite sprite)
        {
            Icon.sprite = sprite;
        }
        
        public void AddEffect(EffectView effect)
        {
            _effects.Add(effect);
            effect.transform.SetParent(EffectContent);
        }

        public void ClearEffects()
        {
            foreach (EffectView effect in _effects)
            {
                Destroy(effect.gameObject);
            }
            
            _effects.Clear();
        }

        public void SetMoney(string text)
        {
            Money.text = text;
        }
    }
}