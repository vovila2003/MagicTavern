using System.Collections.Generic;
using Modules.Pools;
using Tavern.Settings;
using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    public class EntityCardViewPool : IEntityCardViewPool
    {
        private readonly Pool<EntityCardView> _cardPool;
        
        private readonly Dictionary<IEntityCardView, EntityCardView> _cardViews = new();

        public EntityCardViewPool(UISettings settings, Transform pool)
        {
            _cardPool = new Pool<EntityCardView>(
                settings.CommonSettings.EntityCardConfig.EntityCard, 
                settings.CommonSettings.EntityCardConfig.StartPoolLength, 
                settings.CommonSettings.EntityCardConfig.Limit, 
                pool);
        }

        public bool TrySpawnEntityCardViewUnderTransform(Transform viewContentTransform, out IEntityCardView view)
        {
            if (_cardPool.TrySpawn(out EntityCardView cardView))
            {
                view = cardView;
                cardView.SetParent(viewContentTransform);
                _cardViews.Add(view, cardView);
                
                return true;
            }
            
            view = null;
            
            return false;
        }

        public void UnspawnEntityCardView(IEntityCardView view)
        {
            if (_cardViews.Remove(view, out EntityCardView cardView))
            {
                _cardPool.Unspawn(cardView);
            }
        }
    }
}