using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentGroupEggs : ComponentFilterAnimalProductGroup
    {
        public override IItemComponent Clone()
        {
            return new ComponentGroupEggs();
        }
    }
}