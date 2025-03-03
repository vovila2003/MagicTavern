using System;
using Modules.Inventories;
using Modules.Items;
using Tavern.Components;
using Tavern.Gardening;

namespace Tavern.UI.Presenters
{
    public class SeedItemsPresenter : ItemsPresenter<SeedItem>
    {
        public event Action OnSeeded;
        private readonly SeederComponent _seeder;

        private Pot _pot;

        public SeedItemsPresenter(
            IContainerView view, 
            CommonPresentersFactory commonPresentersFactory, 
            IInventory<SeedItem> inventory,
            SeederComponent seeder
            ) : base(view, commonPresentersFactory, inventory)
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
            _seeder.Seed(_pot, item.Config as SeedItemConfig);
            OnSeeded?.Invoke();
        }
        
        protected override void OnRightClick(Item item)
        {
            _seeder.Seed(_pot, item.Config as SeedItemConfig);
            OnSeeded?.Invoke();
        }
    }
}