using System;
using Modules.Items;
using UnityEngine;

namespace Tavern.Cooking
{
    [CreateAssetMenu(
        fileName = "KitchenItemConfig",
        menuName = "Settings/Cooking/KitchenItems/Kitchen Item Config")]
    public class KitchenItemConfig : ItemConfig
    {
        public override Item Create()
        {
            return new KitchenItem(this, GetComponentClones(), Array.Empty<IExtraItemComponent>());
        }

        protected override string GetItemType() => nameof(KitchenItem);
    }
}