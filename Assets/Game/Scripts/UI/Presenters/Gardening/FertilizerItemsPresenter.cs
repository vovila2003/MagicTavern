using System;
using Modules.Items;
using Tavern.Components;
using Tavern.Gardening;
using Tavern.Gardening.Fertilizer;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class FertilizerItemsPresenter : ItemsPresenter<FertilizerItem>
    {
        private const string Fertilize = "Удобрить";
        
        public event Action<bool> OnFertilized;
        private readonly Seeder _seeder;

        private Pot _pot;

        public FertilizerItemsPresenter(
            IContainerView view, 
            CommonPresentersFactory commonPresentersFactory, 
            FertilizerInventoryContext fertilizerInventoryContext,
            Seeder seeder,
            Func<Transform, InfoPresenter> infoPresenterFactory,
            Transform canvas
            ) : base(view, commonPresentersFactory, fertilizerInventoryContext.Inventory, 
                     infoPresenterFactory, canvas, InfoPresenter.Mode.Dialog)
        {
            _seeder = seeder;
            ActionName = Fertilize;
        }

        public void Show(Pot pot)
        {
            _pot = pot;
            Show();
        }

        protected override void OnShow()
        {
            base.OnShow();
            bool active = _pot.IsSeeded && !_pot.IsFertilized;
            SetActive(active);
        }

        protected override void OnRightClick(Item item)
        {
            bool result = _seeder.Fertilize(_pot, item.Config as FertilizerConfig);
            OnFertilized?.Invoke(result);
        }
    }
}