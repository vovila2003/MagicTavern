using Modules.Items;
using Tavern.Effects;

namespace Tavern.Infrastructure
{
    public class ComponentEffectSerializer : IExtraSerializer
    {
        private readonly EffectsCatalog _effectsCatalog;

        public ComponentEffectSerializer(EffectsCatalog effectsCatalog)
        {
            _effectsCatalog = effectsCatalog;
        }

        public string Serialize(IExtraItemComponent extraComponent)
        {
            return extraComponent is ComponentEffect componentEffect 
                ? componentEffect.Config.EffectName 
                : string.Empty;
        }

        public void Deserialize(Item item, string value)
        {
            if (!_effectsCatalog.TryGetEffect(value, out EffectConfig effectConfig)) return;
            
            item.AddExtraComponent(new ComponentEffect(effectConfig));
        }
    }
}