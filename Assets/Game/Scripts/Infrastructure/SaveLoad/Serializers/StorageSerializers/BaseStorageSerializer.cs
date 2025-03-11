using System.Collections.Generic;
using Modules.SaveLoad;
using Modules.Storages;

namespace Tavern.Infrastructure
{
    public abstract class BaseStorageSerializer : IGameSerializer
    {
        private readonly string _name;
        private readonly ResourceStorage _storage;

        protected BaseStorageSerializer(ResourceStorage storage, string name)
        {
            _name = name;
            _storage = storage;
        }

        public void Serialize(IDictionary<string, string> saveState)
        {
            saveState[_name] = _storage.Value.ToString();
        }

        public void Deserialize(IDictionary<string, string> loadState)
        {
            if (!loadState.TryGetValue(_name, out string valueString)) return;
            
            if (int.TryParse(valueString, out int value))
            {
                _storage.Set(value);
            }
        }
    }
}