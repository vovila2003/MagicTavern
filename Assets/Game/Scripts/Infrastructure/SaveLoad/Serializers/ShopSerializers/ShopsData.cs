using System;
using System.Collections.Generic;

namespace Tavern.Infrastructure
{
    [Serializable]
    public class ShopsData
    {
        public List<ShopData> Shops;

        public ShopsData(int count)
        {
            Shops = new List<ShopData>(count);
        }
    }
    
    [Serializable]
    public class ShopData
    {
        public float[] Position;
        public float[] Rotation;
        public string ConfigName;
        public int Money;
        public int Reputation;
        public List<ItemConfigData> Items;
        public List<ItemData> CharacterItems;
    }
    
    [Serializable]
    public class ItemConfigData
    {
        public string Name;
        public int Count;
        public int Price;
    }
}