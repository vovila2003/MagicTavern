using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentGroupEggs : ComponentGroup
    {
        public override IItemComponent Clone()
        {
            return new ComponentGroupEggs();
        }
    }
}