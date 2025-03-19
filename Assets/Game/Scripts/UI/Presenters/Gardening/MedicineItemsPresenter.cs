using System;
using Modules.Items;
using Tavern.Gardening;
using Tavern.Gardening.Medicine;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class MedicineItemsPresenter : ItemsPresenter<MedicineItem>
    {
        private const string Heal = "Вылечить";
        
        public event Action<bool> OnHeal;
        
        private readonly Seeder _seeder;
        private Pot _pot;

        public MedicineItemsPresenter(
            IContainerView view, 
            CommonPresentersFactory commonPresentersFactory, 
            MedicineInventoryContext medicineInventoryContext,
            Seeder seeder,
            Func<Transform, InfoPresenter> infoPresenterFactory,
            Transform canvas
            ) : base(view, commonPresentersFactory, medicineInventoryContext.Inventory, 
                     infoPresenterFactory, canvas, InfoPresenter.Mode.Dialog)
        {
            _seeder = seeder;
            ActionName = Heal;
        }

        public void Show(Pot pot)
        {
            _pot = pot;
            Show();
        }

        protected override void OnShow()
        {
            base.OnShow();
            SetActive(_pot.IsSeeded);
        }

        protected override void OnRightClick(Item item)
        {
            bool result = _seeder.Heal(_pot, item.Config as MedicineConfig);
            OnHeal?.Invoke(result);
        }
    }
}