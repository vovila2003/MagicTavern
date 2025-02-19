using System;
using Modules.Items;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentFilterAnimalProductGroup : IComponentGroup
    {
        public virtual string Name => "Животные продукты";

        public virtual IItemComponent Clone()
        {
            return new ComponentFilterAnimalProductGroup();
        }
    }
}