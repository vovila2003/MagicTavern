using System;
using Tavern.Effects;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class EffectsSettings
    {
        [field: SerializeField]
        public EffectsCatalog EffectsCatalog { get; private set; }
    }
}