using Modules.Shopping;
using Sirenix.OdinInspector;
using Tavern.Buying;
using UnityEngine;
using VContainer;

namespace Tavern.Shop
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] 
        private GoodsCatalog Catalog;

        private GoodsBuyer _buyer;

        [Inject]
        private void Construct(GoodsBuyer buyer)
        {
            _buyer = buyer;
        }
        
        [Button]
        public void ShowGoodsInfo(GoodsConfig goodsConfig)
        {
            Debug.Log($"Name {goodsConfig.Name}");
            Debug.Log($"Title {goodsConfig.Goods.GoodsMetadata.Title}");
            Debug.Log($"Description {goodsConfig.Goods.GoodsMetadata.Description}");
            Debug.Log($"Price {goodsConfig.Goods.GoodsPrice}");
        }

        [Button]
        public void TryBuyByName(string goodsName)
        {
            if (!Catalog.TryGetGoods(goodsName, out GoodsConfig goods))
            {
                Debug.Log($"Goods with name {goodsName} not found");
                return;
            }
            
            Buy(goods);
        }

        [Button]
        public void Buy(GoodsConfig goodsConfig)
        {
            if (!_buyer.CanBuyGoods(goodsConfig.Goods))
            {
                Debug.Log($"Can't buy goods {goodsConfig.Name}");
                return;
            }
            
            _buyer.BuyGoods(goodsConfig);
        }
    }
}