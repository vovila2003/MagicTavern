using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Goods
{
    public class GoodsTester : MonoBehaviour
    {
        [Button]
        public void Test(GoodsConfig goodsConfig)
        {
            Debug.Log($"Name {goodsConfig.Name}");
            Debug.Log($"Title {goodsConfig.Goods.GoodsMetadata.Title}");
            Debug.Log($"Description {goodsConfig.Goods.GoodsMetadata.Description}");
            Debug.Log($"Count {goodsConfig.Goods.GoodsComponent.Count}");
        }
    }
}