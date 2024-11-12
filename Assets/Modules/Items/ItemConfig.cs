using UnityEngine;

namespace Modules.Items
{
    [CreateAssetMenu(
        fileName = "ItemConfig",
        menuName = "Settings/Items/New ItemConfig"
    )]
    public class ItemConfig : ScriptableObject
    {
        [SerializeField]
        public Item Item;

        private void OnValidate() => Item.Attributes ??= new object[] { };
    }
}