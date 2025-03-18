using System;
using System.Collections.Generic;

namespace Tavern.Infrastructure
{
    [Serializable]
    public class ItemData
    {
        public string Name;
        public int Count;
        public List<ExtraData> Extras;
    }
    
    [Serializable]
    public class ExtraData
    {
        public string Name;
        public string Data;
    }
}