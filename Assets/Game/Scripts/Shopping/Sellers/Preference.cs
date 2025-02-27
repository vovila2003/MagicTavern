using System;
using Modules.Shopping;
using UnityEngine;

namespace Tavern.Shopping
{
    [Serializable]
    public class Preference
    {
        [SerializeField]
        public ComponentGroupConfig Group;

        public float Factor;
    }
}