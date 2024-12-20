using UnityEngine;

namespace Tavern.Goods
{
    [CreateAssetMenu(
        fileName = "GoodsConfig",
        menuName = "Settings/Shopping/Goods Item Config")]
    public class GoodsConfig : ScriptableObject
    {
        [SerializeField]
        public Goods Goods;
        
        public string Name => Goods.Name;
    }
}