using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentGroupDairy : ComponentGroup
    {
        public override IItemComponent Clone()
        {
            return new ComponentGroupDairy();
        }
    }
}