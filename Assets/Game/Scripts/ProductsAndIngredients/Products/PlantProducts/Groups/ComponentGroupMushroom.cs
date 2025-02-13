using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentGroupMushroom : ComponentGroup
    {
        public override IItemComponent Clone()
        {
            return new ComponentGroupMushroom();
        }
    }
}