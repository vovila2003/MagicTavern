using System;
using Modules.Inventories;
using Modules.Items;
using Tavern.Gardening;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class SeedMakerSeedsPresenter : ItemsPresenter<SeedItem>
    {
        public SeedMakerSeedsPresenter(
            IContainerView view,
            CommonPresentersFactory commonPresentersFactory,
            IInventory<SeedItem> inventory,
            Func<Transform, InfoPresenter> infoPresenterFactory,
            Transform canvas
        ) : base(view, commonPresentersFactory, inventory, infoPresenterFactory, canvas, InfoPresenter.Mode.Info)
        {
            
        }
        
        protected override void OnRightClick(Item item)
        {
            
        }
    }
}