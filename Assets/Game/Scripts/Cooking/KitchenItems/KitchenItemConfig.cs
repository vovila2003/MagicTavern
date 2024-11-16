using Modules.Items;
using UnityEngine;

namespace Tavern.Cooking
{
    [CreateAssetMenu(
        fileName = "KitchenItemConfig",
        menuName = "Settings/Cooking/New KitchenItemConfig")]
    public class KitchenItemConfig : ItemConfig<KitchenItem>
    {
    }
}