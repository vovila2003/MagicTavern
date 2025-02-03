using JetBrains.Annotations;
using Tavern.ProductsAndIngredients;

namespace Tavern.Goods
{
    [UsedImplicitly]
    public sealed class PlantProductComponent : ItemComponent<PlantProductItem>
    {
        public override string Name => PlantProductNameProvider.GetName(base.Name);
    }
}