using Modules.Items;

namespace Tavern.Cooking
{
    public interface IEffectComponent : IItemComponent
    {
        public IEffectConfig Config { get; }
    }
}