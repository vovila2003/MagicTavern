using Tavern.Gardening.Medicine;

namespace Tavern.UI.Presenters
{
    public class MedicineItemsPresenter : ItemsPresenter<MedicineItem>
    {
        private readonly MedicineInventoryContext _medicineInventoryContext;
        
        public MedicineItemsPresenter(
            IContainerView view, 
            CommonPresentersFactory commonPresentersFactory, 
            MedicineInventoryContext medicineInventoryContext) 
            : base(view, commonPresentersFactory, medicineInventoryContext.Inventory)
        {
            _medicineInventoryContext = medicineInventoryContext;
        }
    }
}