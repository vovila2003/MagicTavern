using System.Collections.Generic;
using Modules.Pools;
using Tavern.Settings;
using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    public class ItemCardViewPool : IItemCardViewPool
    {
        private readonly Pool<ItemCardView> _cardPool;
        
        private readonly Dictionary<IItemCardView, ItemCardView> _cardViews = new();

        public ItemCardViewPool(UISettings settings, Transform pool)
        {
            _cardPool = new Pool<ItemCardView>(
                settings.CommonSettings.ItemCardConfig.ItemCard, 
                settings.CommonSettings.ItemCardConfig.StartPoolLength, 
                settings.CommonSettings.ItemCardConfig.Limit, 
                pool);
        }

        public bool TrySpawnItemCardViewUnderTransform(Transform viewContentTransform, out IItemCardView view)
        {
            if (_cardPool.TrySpawn(out ItemCardView cardView))
            {
                view = cardView;
                view.SetParent(viewContentTransform);
                _cardViews.Add(view, cardView);
                
                return true;
            }
            
            view = null;
            
            return false;
        }

        public void UnspawnItemCardView(IItemCardView view)
        {
            if (_cardViews.Remove(view, out ItemCardView cardView))
            {
                _cardPool.Unspawn(cardView);
            }
        }
    }
}