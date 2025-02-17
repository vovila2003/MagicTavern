using System;
using Tavern.Shopping;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class ShoppingSettings
    {
        [field: SerializeField]
        public Shop ShopPrefab { get; private set; }
    }
}