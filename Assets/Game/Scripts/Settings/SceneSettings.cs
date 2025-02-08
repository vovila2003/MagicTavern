using System;
using Tavern.Cooking;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class SceneSettings
    {
        [SerializeField] 
        private Transform World;

        [SerializeField] 
        private Transform Pots;

        [SerializeField] 
        private Transform KitchenItemsParent;
        
        [SerializeField]
        private KitchenItemPoint[] KitchenItemPoints;
        
        public Transform WorldTransform => World;
        public Transform PotsTransform => Pots;
        public Transform KitchenParent => KitchenItemsParent;
        public KitchenItemPoint[] KitchenPoints => KitchenItemPoints;
    }
}