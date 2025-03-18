using System;
using Modules.Items;
using UnityEngine;

namespace Tavern.Storages
{
    [CreateAssetMenu(
        fileName = "SlopsConfig",
        menuName = "Settings/Resources/Slops Config")]
    public class SlopsItemConfig : ItemConfig
    {
        public override Item Create()
        {
            return new SlopsItem(this, GetComponentClones(), Array.Empty<IExtraItemComponent>());
        }

        protected override string GetItemType() => nameof(SlopsItem);
    }
}