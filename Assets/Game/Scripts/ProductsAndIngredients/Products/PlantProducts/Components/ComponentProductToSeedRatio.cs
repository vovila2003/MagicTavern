using System;
using Modules.Items;
using UnityEngine;

namespace Tavern.ProductsAndIngredients
{
    [Serializable]
    public class ComponentProductToSeedRatio : IItemComponent
    {
        [field: SerializeField] 
        public int ProductToSeedRatio { get; private set; }

        public ComponentProductToSeedRatio()
        {
        }

        public ComponentProductToSeedRatio(int ratio)
        {
            ProductToSeedRatio = ratio;
        }

        public IItemComponent Clone()
        {
            return new ComponentProductToSeedRatio(ProductToSeedRatio);
        }
    }
}