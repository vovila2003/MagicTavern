using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentGroupFruit : ComponentFilterPlantProductGroup
    {
        public override string Name => "Фрукты";
        public override IItemComponent Clone()
        {
            return new ComponentGroupFruit();
        }
    }
}