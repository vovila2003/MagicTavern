using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentGroupBerry : ComponentFilterPlantProductGroup
    {
        public override IItemComponent Clone()
        {
            return new ComponentGroupBerry();
        }
    }
}