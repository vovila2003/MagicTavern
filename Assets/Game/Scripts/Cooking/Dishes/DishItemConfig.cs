using Modules.Items;
using UnityEngine;

namespace Tavern.Cooking
{
    [CreateAssetMenu(
        fileName = "DishItemConfig",
        menuName = "Settings/Cooking/New DishItemConfig")]
    public class DishItemConfig : ItemConfig<DishItem>
    {
    }
}