using Modules.Items;
using UnityEngine;

namespace Modules.Looting
{
    [CreateAssetMenu(
        fileName = "LootItemsCatalog", 
        menuName = "Settings/Looting/LootItems Catalog")]
    public class LootItemsCatalog : ItemsCatalog<LootItem>
    {
    }
}