using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface IItemCardViewPool
    {
        bool TrySpawnItemCardViewUnderTransform(Transform viewContentTransform, out IItemCardView view);
        void UnspawnItemCardView(IItemCardView view);
    }
}