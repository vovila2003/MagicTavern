// using Sirenix.OdinInspector;
// using Tavern.Shopping.Buying;
// using UnityEngine;
// using VContainer;
//
// namespace Tavern.Shopping.Shop
// {
//     public class Shop : MonoBehaviour
//     {
//         private GoodsBuyer _buyer;
//
//         [Inject]
//         private void Construct(GoodsBuyer buyer)
//         {
//             _buyer = buyer;
//         }
//         
//         [Button]
//         public void TryBuyByName(string goodsName)
//         {
//             if (!Catalog.TryGetGoods(goodsName, out GoodsConfig goods))
//             {
//                 Debug.Log($"Goods with name {goodsName} not found");
//                 return;
//             }
//             
//             Buy(goods);
//         }
//
//         [Button]
//         public void Buy(GoodsConfig goodsConfig)
//         {
//             if (!_buyer.CanBuyGoods(goodsConfig.Goods))
//             {
//                 Debug.Log($"Can't buy goods {goodsConfig.Name}");
//                 return;
//             }
//             
//             _buyer.BuyGoods(goodsConfig);
//         }
//     }
// }