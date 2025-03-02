using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface IGardeningViewsFactory
    {
        IContainerView CreateSeedItemsView(Transform viewContainer);
        IContainerView CreateFertilizerItemsView(Transform viewContainer);
        IContainerView CreateMedicineItemsView(Transform viewContainer);
    }
}