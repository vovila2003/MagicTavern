using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentGroupMushroom : ComponentFilterPlantProductGroup
    {
        public override string Name => "Грибы";
        
        public override IItemComponent Clone()
        {
            return new ComponentGroupMushroom();
        }
    }
}