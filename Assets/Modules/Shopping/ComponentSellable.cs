using Modules.Items;
using UnityEngine;

namespace Modules.Shopping
{
    public class ComponentSellable : IItemComponent
    {
        [field: SerializeField]
        public int BasePrice { get; private set; }
        
        public ComponentSellable()
        {
        }
        
        public ComponentSellable(int basePrice)
        {
            BasePrice = basePrice;
        }

        public IItemComponent Clone()
        {
            return new ComponentSellable(BasePrice);
        }
    }
}