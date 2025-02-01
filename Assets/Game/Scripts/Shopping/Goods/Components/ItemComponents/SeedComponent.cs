using JetBrains.Annotations;
using Tavern.Gardening;

namespace Tavern.Goods
{
    [UsedImplicitly]
    public sealed class SeedComponent : ItemComponent<SeedItem>
    {
        public override string Name => SeedNameProvider.GetName(base.Name);
    }
}