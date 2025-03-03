using System;
using System.Net.Mime;
using Modules.Items;
using Tavern.Components;
using Tavern.Gardening;
using Tavern.Gardening.Fertilizer;

namespace Tavern.UI.Presenters
{
    public class FertilizerItemsPresenter : ItemsPresenter<FertilizerItem>
    {
        public event Action OnFertilized;
        private readonly SeederComponent _seeder;
        private Pot _pot;

        public FertilizerItemsPresenter(
            IContainerView view, 
            CommonPresentersFactory commonPresentersFactory, 
            FertilizerInventoryContext fertilizerInventoryContext,
            SeederComponent seeder
            ) : base(view, commonPresentersFactory, fertilizerInventoryContext.Inventory)
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
            _seeder.Fertilize(_pot, item.Config as FertilizerConfig);
            OnFertilized?.Invoke();
        }
        
        protected override void OnRightClick(Item item)
        {
            _seeder.Fertilize(_pot, item.Config as FertilizerConfig);
            OnFertilized?.Invoke();
        }
    }
}