using Modules.Items;

namespace Tavern.Effects
{
    public interface IEffectComponent : IItemComponent
    {
        public IEffectConfig Config { get; }
    }
}