using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentGroupMushroom : ComponentPlantProductGroup
    {
        public override IItemComponent Clone()
        {
            return new ComponentGroupMushroom();
        }
    }
}