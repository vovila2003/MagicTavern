using System;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public sealed class TimeSettings
    {
        [field: SerializeField]
        public int SecondsIn12Hours { get; private set; }
    }
}