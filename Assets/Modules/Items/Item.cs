using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace Modules.Items
{
    [Serializable]
    public class Item
    {
        protected ItemConfig Config;
        
        [ShowInInspector, ReadOnly]
        public string ItemName => Config.Name;
        
        [ShowInInspector, ReadOnly]
        public ItemFlags ItemFlags => Config.Flags;
        
        [ShowInInspector, ReadOnly]
        public Metadata Metadata => Config.Metadata;
        
        [ShowInInspector, ReadOnly]
        private List<IItemComponent> _components;

        public Item(
            ItemConfig config,
            params IItemComponent[] attributes)
        {
            Config = config;
            _components = new List<IItemComponent>(attributes);
        }

        public void AddComponent(IItemComponent component)
        {
            _components.Add(component);
        }
        
        public T Get<T>()
        {
            foreach (IItemComponent attribute in _components)
            {
                if (attribute is T tAttribute)
                {
                    return tAttribute;
                }
            }

            throw new Exception($"Attribute of type {typeof(T).Name} is not found!");
        }

        public List<T> GetAll<T>()
        {
            var result = new List<T>();
            foreach (IItemComponent attribute in _components)
            {
                if (attribute is T tAttribute)
                {
                    result.Add(tAttribute);
                }
            }
            
            return result;
        }

        public bool Has<T>() => _components.OfType<T>().Any();

        public virtual Item Clone()
        {
            IItemComponent[] components = GetComponents();

            return new Item(Config, components);
        }

        protected IItemComponent[] GetComponents()
        {
            int count = _components.Count;
            var components = new IItemComponent[count];

            for (var i = 0; i < count; i++)
            {
                components[i] = _components[i].Clone();
            }

            return components;
        }
    }
}