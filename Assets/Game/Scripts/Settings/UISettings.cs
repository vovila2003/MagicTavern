using System;
using Tavern.UI;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class UISettings
    {
        [SerializeField]    
        private EntityCardView EntityCardPrefab;

        public EntityCardView EntityCard => EntityCardPrefab;
    }
}