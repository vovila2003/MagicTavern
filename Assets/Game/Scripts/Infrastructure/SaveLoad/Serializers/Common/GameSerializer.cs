using System.Collections.Generic;
using Modules.SaveLoad;
using Tavern.Utils;

namespace Tavern.Infrastructure
{
    public abstract class GameSerializer<TData> : IGameSerializer
    {
        protected virtual string Key => typeof(TData).Name;
        
        public void Serialize(IDictionary<string, string> saveState)
        {
            TData data = Serialize();
            saveState[Key] = Serializer.SerializeObject(data);
        }

        public void Deserialize(IDictionary<string, string> loadState)
        {
            if (!loadState.TryGetValue(Key, out string json)) return;
        
            (TData data, bool ok) = Serializer.DeserializeObject<TData>(json);
            if (!ok) return;
            
            Deserialize(data);
        }

        protected abstract TData Serialize();
        protected abstract void Deserialize(TData data);
    }
}