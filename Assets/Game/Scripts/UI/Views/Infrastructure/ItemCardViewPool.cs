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

        public ItemCardViewPool(UISettings settings, Transform itemCardViewPoolParent)
        {
            _cardPool = new Pool<ItemCardView>(
                settings.ItemCardConfig.ItemCard, 
                settings.ItemCardConfig.StartPoolLength, 
                settings.ItemCardConfig.Limit, 
                itemCardViewPoolParent);
        }

        public bool TrySpawnItemCardViewUnderTransform(Transform viewContentTransform, out IItemCardView view)
        {
            if (_cardPool.TrySpawn(out ItemCardView cardView))
            {
                view = cardView;
                cardView.SetParent(viewContentTransform);
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