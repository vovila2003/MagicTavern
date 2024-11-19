using System;
using Modules.Gardening;
using UnityEngine;

namespace Tavern.Cooking
{
    [Serializable]
    public class ProductIngredient
    {
        [SerializeField] 
        private PlantConfig Product;

        [SerializeField] 
        private int Amount;
        
        public Plant Type => Product.Plant;
        public int ProductAmount => Amount;
    }
}