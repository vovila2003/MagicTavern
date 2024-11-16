using Modules.Items;
using UnityEngine;

namespace Modules.Cooking
{
    [CreateAssetMenu(
        fileName = "KitchenItemConfig",
        menuName = "Settings/Cooking/New KitchenItemConfig")]
    public class KitchenItemConfig : ItemConfig<KitchenItem>
    {
    }
}