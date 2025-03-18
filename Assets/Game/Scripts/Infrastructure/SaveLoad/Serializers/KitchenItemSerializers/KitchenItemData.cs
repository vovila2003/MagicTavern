using System;
using System.Collections.Generic;

namespace Tavern.Infrastructure
{
    [Serializable]
    public class KitchenItemsData
    {
        public List<KitchenItemData> Items;

        public KitchenItemsData(int count)
        {
            Items = new List<KitchenItemData>(count);
        }
    }
    
    [Serializable]
    public class KitchenItemData
    {
        public float[] Position;
        public float[] Rotation;
        public string ConfigName;
    }
}