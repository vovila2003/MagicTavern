using UnityEngine;

namespace Tavern.Cooking
{
    [CreateAssetMenu(
        fileName = "DishItemEffect",
        menuName = "Settings/Cooking/Effects/Dish Item Effect")]
    public class EffectOneConfig : ScriptableObject, IEffectConfig
    {
        [SerializeField] 
        private Sprite EffectIcon;
        
        public Sprite Icon => EffectIcon;
    }
}