using System;
using Tavern.ProductsAndIngredients;
using UnityEngine;

namespace Tavern.Cooking
{
    [Serializable]
    public class AnimalProductIngredient
    {
        public AnimalProductItemConfig AnimalProductConfig;
        public bool FromGroup;

        public string Name()
        {
            if (!FromGroup)
                return AnimalProductConfig.GetItem().ItemName;

            if (AnimalProductConfig.GetItem().TryGet(out GroupComponent groupComponent))
                return groupComponent.GroupName;
            
            Debug.LogError("Animal ingredient marked as Grouped, but haven't group component");
            
            return string.Empty;
        }
    }
}