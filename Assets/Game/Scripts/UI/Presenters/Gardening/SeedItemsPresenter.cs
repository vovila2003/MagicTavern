using Modules.Inventories;
using Tavern.Gardening;

namespace Tavern.UI.Presenters
{
    public class SeedItemsPresenter : ItemsPresenter<SeedItem>
    {
        public SeedItemsPresenter(
            IContainerView view, 
            CommonPresentersFactory commonPresentersFactory, 
            IInventory<SeedItem> inventory
            ) : base(view, commonPresentersFactory, inventory)
        {
        }
    }
}