using System;
using Sirenix.OdinInspector;
using Tavern.Cooking;
using Tavern.Gardening;
using Tavern.Shopping;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class SceneSettings
    {
        [SerializeField] 
        private Transform World;

        [SerializeField] 
        private Transform KitchenItemsParent;
        
        [SerializeField]
        private KitchenItemPoint[] KitchenItemPoints;
        
        [field: SerializeField]
        public Transform ShopsParent { get; private set; }
        
        [field: SerializeField]
        public ShopPoint[] ShopPoints { get; private set; }
        
        [field: SerializeField]
        public Transform PotsParent { get; private set; }
        
        [field: SerializeField]
        public PotPoint[] PotPoints { get; private set; }
        
        [field: SerializeField]
        public GroundSettings GroundSettings { get; private set; }
        
        [field: SerializeField] 
        public MinimapSettings MinimapSettings { get; private set; }
        
        [field: SerializeField] 
        public UISceneSettings UISceneSettings { get; private set; }
        
        public Transform WorldTransform => World;
        public Transform KitchenParent => KitchenItemsParent;
        public KitchenItemPoint[] KitchenPoints => KitchenItemPoints;
    }
}