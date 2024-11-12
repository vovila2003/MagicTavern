using UnityEngine;

namespace Modules.Cooking
{
    [CreateAssetMenu(
        fileName = "KitchenItemConfig",
        menuName = "Settings/Cooking/New KitchenItemConfig"
    )]
    public class KitchenItemConfig : ScriptableObject
    {
        [SerializeField]
        public KitchenItem KitchenItem;
        
        private void OnValidate() => KitchenItem.Attributes ??= new object[] { };
    }
}