using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentGroupMeat : ComponentFilterAnimalProductGroup
    {
        public override string Name => "Мясо";
        public override IItemComponent Clone()
        {
            return new ComponentGroupMeat();
        }
    }
}