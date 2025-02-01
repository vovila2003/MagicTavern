using JetBrains.Annotations;
using Tavern.Gardening;

namespace Tavern.Goods
{
    [UsedImplicitly]
    public sealed class ProductComponent : ItemComponent<ProductItem>
    {
        public override string Name => ProductNameProvider.GetName(base.Name);
    }
}