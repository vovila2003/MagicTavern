using System.Collections.Generic;
using Modules.Items;
using UnityEngine;

namespace Tavern.Shopping
{
    [CreateAssetMenu(
        fileName = "SellerConfig",
        menuName = "Settings/Shopping/Seller Config")]
    public class SellerConfig : ScriptableObject 
    {
        [field: SerializeField]
        public string Name { get; private set; }
        
        [field: SerializeField]
        public int StartMoney { get; private set; }
        
        [field: SerializeField]
        public int WeeklyMoneyBonus { get; private set; }
        
        [field: SerializeField]
        public Metadata Metadata { get; private set; }

        [field: SerializeField]
        public ItemConfig[] StartItems { get; private set; }
        
        [field: SerializeReference]
        public List<ComponentGroup> Filter { get; private set; }
        
        [field: SerializeField]
        public Preference[] Preferences { get; private set; }
        
        [field: SerializeField]
        public int[] Discounts { get; private set; } = new int[7];
        
        [field: SerializeField]
        public ItemConfig[] Assortment { get; private set; }
        
        private void Awake()
        {
            Filter ??= new List<ComponentGroup>();
        }

        private void OnValidate()
        {
            Filter ??= new List<ComponentGroup>();
        }
    }
}