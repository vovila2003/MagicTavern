using System;
using Modules.Gardening;
using UnityEngine;

namespace Tavern.Cooking
{
    [Serializable]
    public class ProductIngredient
    {
        [SerializeField] 
        private PlantType ProductType;

        [SerializeField] 
        private int Amount;
        
        public PlantType Type => ProductType;
        public int ProductAmount => Amount;
    }
}