using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentGroupEggs : ComponentAnimalProductGroup
    {
        public override IItemComponent Clone()
        {
            return new ComponentGroupEggs();
        }
    }
}