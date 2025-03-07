using Modules.Items;
using Tavern.Gardening;
using UnityEngine;

namespace Tavern.ProductsAndIngredients
{
    [CreateAssetMenu(
        fileName = "PlantProduct",
        menuName = "Settings/Products/Plant Product Config")]
    public class PlantProductItemConfig : PlantItemConfig
    {
        [field: SerializeField] 
        public int ProductToSeedRatio { get; private set; } = 2;

        public override void OnValidate()
        {
            if (!TryGet(out ComponentPlant component)) return;
            
            if (component.Config is null) return;
            
            SetName(PlantProductNameProvider.GetName(component.Config.Name));
        }

        public override Item Create()
        {
            return new PlantProductItem(this, GetComponentClones());
        }

        protected override string GetItemType() => nameof(PlantProductItem);
    }
}