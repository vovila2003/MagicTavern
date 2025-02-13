using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentGroupBerry : ComponentGroup
    {
        public override IItemComponent Clone()
        {
            return new ComponentGroupBerry();
        }
    }
}