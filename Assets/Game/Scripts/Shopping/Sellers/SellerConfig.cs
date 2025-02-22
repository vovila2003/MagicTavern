using Modules.Info;
using Modules.Items;
using Sirenix.OdinInspector;
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
        
        [field: Space]
        [field: SerializeField]
        public Metadata Metadata { get; private set; }
        
        [field: SerializeField]
        public Metadata ShopMetadata { get; private set; }

        [field: SerializeField]
        public ItemConfig[] StartItems { get; private set; }

        [field: SerializeField]
        public ItemConfig[] Assortment { get; private set; }

        [field: SerializeField]
        public Preference[] Preferences { get; private set; }

        [field: SerializeField]
        public float[] Discounts { get; private set; } = new float[ShoppingConfig.MaxReputation + 1];

        [field: SerializeField]
        public float[] Surcharges { get; private set; } = { 30, 27.15f, 24.3f, 21.45f, 18.6f, 15.75f, 12.9f, 10 };

        public NpcSeller Create()
        {
            return new NpcSeller(this);
        }

        [Button]
        private void Validate()
        {
            OnValidate();
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            
            CheckForSellable(StartItems, "StartItems");
            CheckForSellable(Assortment, "Assortment");
        }

        private void CheckForSellable(ItemConfig[] collection, string collectionName)
        {
            foreach (ItemConfig itemConfig in collection)
            {
                if (itemConfig.Flags.HasFlag(ItemFlags.Sellable)) continue;

                Debug.LogError($"Seller {Name}: item {itemConfig.Name} from {collectionName} is not Sellable");
            }
        }
    }
}