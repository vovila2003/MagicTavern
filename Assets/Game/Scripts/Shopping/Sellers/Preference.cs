using System;
using Modules.Items;
using UnityEngine;

namespace Tavern.Shopping
{
    [Serializable]
    public class Preference
    {
        [SerializeReference]
        public IComponentGroup Group;

        public float Factor;
    }
}