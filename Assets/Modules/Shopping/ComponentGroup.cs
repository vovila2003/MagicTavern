using System;
using Modules.Items;
using UnityEngine;

namespace Modules.Shopping
{
    [Serializable]
    public class ComponentGroup : IComponentGroup
    {
        [field: SerializeField]
        public ComponentGroupConfig Config { get; private set; }

        public ComponentGroup(ComponentGroupConfig config)
        {
            Config = config;
        }

        public ComponentGroup() { }

        public IItemComponent Clone()
        {
            return new ComponentGroup(Config);
        }
    }
}