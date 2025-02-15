using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentGroupBerry : ComponentPlantProductGroup
    {
        public override IItemComponent Clone()
        {
            return new ComponentGroupBerry();
        }
    }
}