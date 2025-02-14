using System.Collections.Generic;
using Modules.Items;
using UnityEngine;

namespace Tavern.Shopping
{
    [CreateAssetMenu(
        fileName = "SellerConfig",
        menuName = "Settings/Shopping/Seller Config")]
    public class SellerConfig : NamedConfig 
    {
        [field: SerializeField]
        public int StartMoney { get; private set; }
        
        [field: SerializeField]
        public int WeeklyMoneyBonus { get; private set; }
        
        [field: SerializeField]
        public Metadata Metadata { get; private set; }

        [field: SerializeField]
        public ItemsCatalog StartItems { get; private set; }
        
        [field: SerializeReference]
        public List<ComponentGroup> Filter { get; private set; }
        
        [field: SerializeField]
        public Preference[] Preferences { get; private set; }
        
        [field: SerializeField]
        public int[] Discounts { get; private set; } = new int[7];
        
        [field: SerializeField]
        public ItemsCatalog Assortment { get; private set; }
        
        private void Awake()
        {
            Filter ??= new List<ComponentGroup>();
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            
            Filter ??= new List<ComponentGroup>();
            
            //TODO check filter and assortment, startItems
        }
    }
}