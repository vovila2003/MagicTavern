using System;
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
        public override void OnValidate()
        {
            base.OnValidate();
            
            if (!TryGet(out ComponentPlant component)) return;
            
            if (component.Config is null) return;
            
            SetName(PlantProductNameProvider.GetName(component.Config.Name));
        }

        protected override void Awake()
        {
            base.Awake();
            
            if (Has<ComponentProductToSeedRatio>()) return;
            
            Components?.Add(new ComponentProductToSeedRatio(2));
        }

        public override Item Create()
        {
            return new PlantProductItem(this, GetComponentClones(), 
                Array.Empty<IExtraItemComponent>());
        }

        protected override string GetItemType() => nameof(PlantProductItem);
    }
}