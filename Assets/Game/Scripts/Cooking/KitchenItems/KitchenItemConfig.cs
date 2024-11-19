using Modules.Items;
using UnityEngine;

namespace Tavern.Cooking
{
    [CreateAssetMenu(
        fileName = "KitchenItemConfig",
        menuName = "Settings/Cooking/KitchenItems/Kitchen Item Config")]
    public class KitchenItemConfig : ItemConfig<KitchenItem>
    {
    }
}