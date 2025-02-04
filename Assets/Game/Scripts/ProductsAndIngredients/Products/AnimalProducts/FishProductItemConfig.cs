using UnityEngine;

namespace Tavern.ProductsAndIngredients
{
    [CreateAssetMenu(
        fileName = "FishProduct",
        menuName = "Settings/Products/Fish Product Config")]
    public class FishProductItemConfig : AnimalProductItemConfig
    {
        private const string Fish = "FishProduct";

        protected override void Awake()
        {
            base.Awake();

            AnimalProductItem item = GetItem();
            if (item.Has<GroupComponent>()) return;

            item.Components?.Add(new GroupComponent()
            {
                GroupName = Fish
            });
        }
    }
}