using Modules.Items;

namespace Tavern.Cooking
{
    public interface IEffectComponent : IItemComponent
    {
        public int Value { get; }
        public IEffectConfig Config { get; }
    }
}