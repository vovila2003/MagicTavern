using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentGroupFish : ComponentFilterAnimalProductGroup
    {
        public override string Name => "Рыба";
        public override IItemComponent Clone()
        {
            return new ComponentGroupFish();
        }
    }
}