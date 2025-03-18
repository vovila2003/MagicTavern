using System;
using Tavern.Looting;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class LootSettings
    {
        [field: SerializeField]
        public LootItemsCatalog LootCatalog { get; private set; }
    }
}