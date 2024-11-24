using System;

namespace Tavern.Gardening.Medicine
{
    [Serializable]
    public class ComponentHarvestHeal : ICloneable
    {
        public ComponentHarvestHeal()
        {
        }

        object ICloneable.Clone() => new ComponentHarvestHeal();
    }
}