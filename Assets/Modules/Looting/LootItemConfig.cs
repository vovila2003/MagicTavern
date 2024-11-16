using Modules.Items;
using UnityEngine;

namespace Modules.Looting
{
    [CreateAssetMenu(
        fileName = "LootItemConfig",
        menuName = "Settings/Looting/New LootItemConfig")]
    public class LootItemConfig : ItemConfig<LootItem>
    {
    }
}