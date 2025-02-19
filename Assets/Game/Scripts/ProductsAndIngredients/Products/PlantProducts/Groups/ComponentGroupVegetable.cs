using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentGroupVegetable : ComponentFilterPlantProductGroup
    {
        public override string Name => "Овощи";
        public override IItemComponent Clone()
        {
            return new ComponentGroupVegetable();
        }
    }
}