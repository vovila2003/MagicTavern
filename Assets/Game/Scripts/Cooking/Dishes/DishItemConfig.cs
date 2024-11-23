using Modules.Items;
using UnityEngine;

namespace Tavern.Cooking
{
    [CreateAssetMenu(
        fileName = "DishItemConfig",
        menuName = "Settings/Cooking/Dishes/Dish Item Config")]
    public class DishItemConfig : ItemConfig<DishItem>
    {
        [SerializeField, Space] 
        private int Exotic;
        
        public int DishExotic => Exotic;
    }
}

