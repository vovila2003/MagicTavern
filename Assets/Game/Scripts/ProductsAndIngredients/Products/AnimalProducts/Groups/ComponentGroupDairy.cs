using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentGroupDairy : ComponentFilterAnimalProductGroup
    {
        public override string Name => "Молочные продукты";

        public override IItemComponent Clone()
        {
            return new ComponentGroupDairy();
        }
    }
}