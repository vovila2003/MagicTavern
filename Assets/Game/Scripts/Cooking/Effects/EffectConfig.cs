using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Cooking
{
    [CreateAssetMenu(
        fileName = "DishItemEffect",
        menuName = "Settings/Cooking/Effects/Dish Item Effect")]
    public class EffectConfig : ScriptableObject, IEffectConfig 
    {
        [SerializeField]
        private string Name;
        
        [SerializeField, PreviewField] 
        private Sprite EffectIcon;

        public string EffectName => Name;
        public Sprite Icon => EffectIcon;
    }
}