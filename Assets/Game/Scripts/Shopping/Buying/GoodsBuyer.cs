using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Modules.Shopping;

namespace Tavern.Buying
{
    public class GoodsBuyer
    {
        public event Action<Modules.Shopping.Goods> OnBuyStarted; 

        public event Action<Modules.Shopping.Goods> OnBuyCompleted;

        private readonly IReadOnlyList<IGoodsBuyCondition> _conditions;

        private readonly IReadOnlyList<IGoodsBuyProcessor> _processors;

        private readonly IReadOnlyList<IGoodsBuyCompleter> _completers;

        public GoodsBuyer(
            IReadOnlyList<IGoodsBuyCondition> checkers,
            IReadOnlyList<IGoodsBuyProcessor> processors,
            IReadOnlyList<IGoodsBuyCompleter> completers)
        {
            _conditions = checkers;
            _processors = processors;
            _completers = completers;
        }
        
        public bool CanBuyGoods(Modules.Shopping.Goods product)
        {
            for (int i = 0, count = _conditions.Count; i < count; i++)
            {
                IGoodsBuyCondition condition = _conditions[i];
                if (!condition.CanBuy(product))
                {
                    return false;
                }
            }

            return true;
        }
        
        [Button]
        public void BuyGoods(GoodsConfig config)
        {
            BuyGoods(config.Goods);
        }

        public void BuyGoods(Modules.Shopping.Goods goods)
        {
            if (!CanBuyGoods(goods))
            {
                Debug.LogWarning($"Can't buy goods {goods.Name}!");
                return;
            }
            
            OnBuyStarted?.Invoke(goods);
            
            //Process buy:
            for (int i = 0, count = _processors.Count; i < count; i++)
            {
                IGoodsBuyProcessor processor = _processors[i];
                processor.ProcessBuy(goods);
            }
            
            //Complete buy:
            for (int i = 0, count = _completers.Count; i < count; i++)
            {
                IGoodsBuyCompleter completer = _completers[i];
                completer.CompleteBuy(goods);
            }
            
            OnBuyCompleted?.Invoke(goods);
        }
    }
}