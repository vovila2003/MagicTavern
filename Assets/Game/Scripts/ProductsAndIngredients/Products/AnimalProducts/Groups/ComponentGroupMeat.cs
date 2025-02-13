using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentGroupMeat : ComponentGroup
    {
        public override IItemComponent Clone()
        {
            return new ComponentGroupMeat();
        }
    }
}