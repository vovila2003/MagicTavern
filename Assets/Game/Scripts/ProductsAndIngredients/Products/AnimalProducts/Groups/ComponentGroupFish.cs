using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentGroupFish : ComponentGroup
    {
        public override IItemComponent Clone()
        {
            return new ComponentGroupFish();
        }
    }
}