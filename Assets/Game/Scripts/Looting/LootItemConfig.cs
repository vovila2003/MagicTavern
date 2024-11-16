using Modules.Items;
using UnityEngine;

namespace Tavern.Looting
{
    [CreateAssetMenu(
        fileName = "LootItemConfig",
        menuName = "Settings/Looting/New LootItemConfig")]
    public class LootItemConfig : ItemConfig<LootItem>
    {
    }
}