using System;
using Sirenix.OdinInspector;

namespace Tavern.Cooking
{
    [Serializable]
    public struct MenuItem
    {
        [InlineEditor]
        public DishItemConfig Dish;
        
        public float Price;
    }
}