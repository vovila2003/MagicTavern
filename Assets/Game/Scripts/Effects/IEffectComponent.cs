using Modules.Items;

namespace Tavern.Effects
{
    public interface IEffectComponent : IExtraItemComponent
    {
        IEffectConfig Config { get; }
    }
}