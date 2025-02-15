using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentGroupMeat : ComponentAnimalProductGroup
    {
        public override IItemComponent Clone()
        {
            return new ComponentGroupMeat();
        }
    }
}