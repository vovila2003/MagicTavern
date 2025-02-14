using Modules.Inventories;
using Modules.Items;
using UnityEngine;

namespace Tavern.Looting
{
    [CreateAssetMenu(
        fileName = "LootItemConfig",
        menuName = "Settings/Looting/Loot Item Config")]
    public class LootItemConfig : StackableItemConfig
    {
        public override Item Create()
        {
            return new LootItem(this, GetComponentClones());
        }
    }
}