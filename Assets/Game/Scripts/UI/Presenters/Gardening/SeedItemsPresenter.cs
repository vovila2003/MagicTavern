using System;
using Modules.Inventories;
using Modules.Items;
using Tavern.Components;
using Tavern.Gardening;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class SeedItemsPresenter : ItemsPresenter<SeedItem>
    {
        private const string Seed = "Посадить";
        public event Action<bool> OnSeeded;
        private readonly Seeder _seeder;

        private Pot _pot;

        public SeedItemsPresenter(
            IContainerView view, 
            CommonPresentersFactory commonPresentersFactory, 
            IInventory<SeedItem> inventory,
            Seeder seeder,
            Func<Transform, InfoPresenter> infoPresenterFactory,
            Transform canvas
            ) : base(view, commonPresentersFactory, inventory, infoPresenterFactory, canvas)
        {
            _seeder = seeder;
            ActionName = Seed;
        }

        public void Show(Pot pot)
        {
            _pot = pot;
            Show();
        }

        protected override void OnShow()
        {
            base.OnShow();
            SetActive(!_pot.IsSeeded);
        }

        protected override void OnRightClick(Item item)
        {
            bool result = _seeder.Seed(_pot, item.Config as SeedItemConfig);
            OnSeeded?.Invoke(result);
        }
    }
}