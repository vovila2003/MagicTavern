using Modules.Inventories;
using UnityEngine;

namespace Tavern.Cooking
{
    [CreateAssetMenu(
        fileName = "DishItemConfig",
        menuName = "Settings/Cooking/Dishes/Dish Item Config")]
    public class DishItemConfig : StackableItemConfig<DishItem>
    {
    }
}

