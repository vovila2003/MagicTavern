using Tavern.Gardening.Fertilizer;

namespace Tavern.UI.Presenters
{
    public class FertilizerItemsPresenter : ItemsPresenter<FertilizerItem>
    {
        private readonly FertilizerInventoryContext _fertilizerInventoryContext;
        
        public FertilizerItemsPresenter(
            IContainerView view, 
            CommonPresentersFactory commonPresentersFactory, 
            FertilizerInventoryContext fertilizerInventoryContext) 
            : base(view, commonPresentersFactory, fertilizerInventoryContext.Inventory)
        {
            _fertilizerInventoryContext = fertilizerInventoryContext;
        }
    }
}