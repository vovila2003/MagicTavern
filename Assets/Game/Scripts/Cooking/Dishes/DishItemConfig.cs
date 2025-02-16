using Modules.Items;
using Modules.Shopping;
using UnityEngine;

namespace Tavern.Cooking
{
    [CreateAssetMenu(
        fileName = "DishItemConfig",
        menuName = "Settings/Cooking/Dishes/Dish Item Config")]
    public class DishItemConfig : SellableItemConfig
    {
        public override Item Create()
        {
            return new DishItem(this, GetComponentClones());
        }

        protected override string GetItemType() => nameof(DishItem);
    }
}

