using System;
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
            return new DishItem(this, GetComponentClones(), Array.Empty<IExtraItemComponent>());
        }

        protected override string GetItemType() => nameof(DishItem);
    }
}

