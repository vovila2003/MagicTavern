using System;
using Modules.Inventories;
using Modules.Items;
using Tavern.ProductsAndIngredients;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class SeedMakerProductItemsPresenter : ItemsPresenter<PlantProductItem>
    {
        public SeedMakerProductItemsPresenter(
            IContainerView view, 
            CommonPresentersFactory commonPresentersFactory, 
            IInventory<PlantProductItem> inventory, 
            Func<Transform, InfoPresenter> infoPresenterFactory, 
            Transform canvas
            ) : base(view, commonPresentersFactory, inventory, infoPresenterFactory, canvas) { }
        
        protected override void OnRightClick(Item item)
        {
            
        }
    }
}