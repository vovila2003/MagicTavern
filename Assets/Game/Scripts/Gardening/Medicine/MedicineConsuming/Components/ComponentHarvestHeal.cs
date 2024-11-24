using System;
using Modules.Items;

namespace Tavern.Gardening.Medicine
{
    [Serializable]
    public class ComponentHarvestHeal : IItemComponent
    {
        public IItemComponent Clone()
        {
            return new ComponentHarvestHeal();
        }
    }
}