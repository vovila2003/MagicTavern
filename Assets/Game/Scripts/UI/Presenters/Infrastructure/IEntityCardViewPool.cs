using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface IEntityCardViewPool
    {
        bool TrySpawnEntityCardViewUnderTransform(Transform viewContentTransform, out IEntityCardView view);
        void UnspawnEntityCardView(IEntityCardView view);
    }
}