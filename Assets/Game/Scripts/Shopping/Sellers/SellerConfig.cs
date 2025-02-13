using System.Collections.Generic;
using Modules.Items;
using UnityEngine;

namespace Tavern.Shopping
{
    
    public abstract class SellerConfig<T> : ScriptableObject where T : Item
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
        public ItemConfig<T>[] StartItems { get; private set; }
        
        [field: SerializeReference]
        public List<ComponentGroup> Filter { get; private set; }
        
        [field: SerializeField]
        public Preference[] Preferences { get; private set; }
        
        [field: SerializeField]
        public int[] Discounts { get; private set; } = new int[7];
        
        [field: SerializeField]
        public ItemConfig<T>[] Assortment { get; private set; }
        
        private void Awake()
        {
            Filter ??= new List<ComponentGroup>();
        }

        private void OnValidate()
        {
            Filter ??= new List<ComponentGroup>();
        }
    }
    
    
    //TODO
    public abstract class SellerConfig<T1, T2> : ScriptableObject 
        where T1 : Item 
        where T2 : Item
    {
        [field: SerializeField]
        public string Name { get; private set; }
        
        [field: SerializeField]
        public int StartMoney { get; private set; }
        
        [field: SerializeField]
        public int WeeklyMoneyBonus { get; private set; }
        
        [field: SerializeField]
        public Metadata Metadata { get; private set; }

        [SerializeField] 
        private ItemConfig<T1>[] StartItems1;
        
        [SerializeField] 
        private ItemConfig<T1>[] StartItems2;
        
        [field: SerializeReference]
        public List<ComponentGroup> Filter { get; private set; }
        
        [field: SerializeField]
        public Preference[] Preferences { get; private set; }
        
        [field: SerializeField]
        public int[] Discounts { get; private set; } = new int[7];

        [SerializeField]
        private ItemConfig<T1>[] Assortment1;
        
        [SerializeField]
        private ItemConfig<T1>[] Assortment2;
        
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