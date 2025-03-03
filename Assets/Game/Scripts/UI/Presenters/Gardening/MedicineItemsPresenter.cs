using System;
using Modules.Items;
using Tavern.Components;
using Tavern.Gardening;
using Tavern.Gardening.Medicine;

namespace Tavern.UI.Presenters
{
    public class MedicineItemsPresenter : ItemsPresenter<MedicineItem>
    {
        public event Action OnHeal;
        
        private readonly SeederComponent _seeder;
        private Pot _pot;

        public MedicineItemsPresenter(
            IContainerView view, 
            CommonPresentersFactory commonPresentersFactory, 
            MedicineInventoryContext medicineInventoryContext,
            SeederComponent seeder
            ) : base(view, commonPresentersFactory, medicineInventoryContext.Inventory)
        {
            _seeder = seeder;
        }

        public void Show(Pot pot)
        {
            _pot = pot;
            Show();
        }

        protected override void OnLeftClick(Item item)
        {
            _seeder.Heal(_pot, item.Config as MedicineConfig);
            OnHeal?.Invoke();
        }
        
        protected override void OnRightClick(Item item)
        {
            _seeder.Heal(_pot, item.Config as MedicineConfig);
            OnHeal?.Invoke();

        }
    }
}