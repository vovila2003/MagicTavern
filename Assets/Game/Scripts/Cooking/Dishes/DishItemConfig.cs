using Modules.Shopping;
using UnityEngine;

namespace Tavern.Cooking
{
    [CreateAssetMenu(
        fileName = "DishItemConfig",
        menuName = "Settings/Cooking/Dishes/Dish Item Config")]
    public class DishItemConfig : SellableItemConfig<DishItem>
    {
    }
}

