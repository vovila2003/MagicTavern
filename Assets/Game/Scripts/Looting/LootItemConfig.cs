using Modules.Items;
using UnityEngine;

namespace Tavern.Looting
{
    [CreateAssetMenu(
        fileName = "LootItemConfig",
        menuName = "Settings/Looting/Loot Item Config")]
    public class LootItemConfig : ItemConfig<LootItem>
    {
    }
}