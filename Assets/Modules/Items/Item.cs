using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Info;
using Sirenix.OdinInspector;

namespace Modules.Items
{
    [Serializable]
    public class Item : IHavingComponentsCapable
    {
        [ShowInInspector, ReadOnly]
        public string ItemName => Config.Name;
        
        [ShowInInspector, ReadOnly]
        public ItemFlags ItemFlags => Config.Flags;
        
        [ShowInInspector, ReadOnly]
        public Metadata Metadata => Config.Metadata;
        
        [ShowInInspector, ReadOnly]
        private List<IItemComponent> _components;
        
        [ShowInInspector, ReadOnly]
        private List<IExtraItemComponent> _extraComponents;

        [ShowInInspector, ReadOnly]
        public string TypeName => Config.ItemTypeName;
        public ItemConfig Config { get; protected set; }

        public Item(
            ItemConfig config,
            IItemComponent[] attributes,
            IExtraItemComponent[] extra)
        {
            Config = config;
            _components = new List<IItemComponent>(attributes);
            _extraComponents = new List<IExtraItemComponent>(extra);
        }

        public void AddExtraComponent(IExtraItemComponent component)
        {
            _extraComponents.Add(component);
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
            
            foreach (IExtraItemComponent attribute in _extraComponents)
            {
                if (attribute is T tAttribute)
                {
                    return tAttribute;
                }
            }

            throw new Exception($"Attribute of type {typeof(T).Name} is not found!");
        }
        
        public bool TryGet<T>(out T component)
        {
            foreach (IItemComponent attribute in _components)
            {
                if (attribute is not T tAttribute) continue;
                
                component = tAttribute;
                return true;
            }
            
            foreach (IExtraItemComponent attribute in _extraComponents)
            {
                if (attribute is not T tAttribute) continue;
                
                component = tAttribute;
                return true;
            }

            component = default;
            return false;
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

        public List<T> GetAllExtra<T>()
        {
            var result = new List<T>();
            foreach (IExtraItemComponent attribute in _extraComponents)
            {
                if (attribute is T tAttribute)
                {
                    result.Add(tAttribute);
                }
            }
            
            return result;
        }

        public bool Has<T>() => _components.OfType<T>().Any() || _extraComponents.OfType<T>().Any();

        public virtual Item Clone()
        {
            IItemComponent[] components = GetComponents();
            IExtraItemComponent[] extraComponents = GetExtraComponents();

            return new Item(Config, components, extraComponents);
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

        protected IExtraItemComponent[] GetExtraComponents()
        {
            int count = _extraComponents.Count;
            var components = new IExtraItemComponent[count];

            for (var i = 0; i < count; i++)
            {
                components[i] = _extraComponents[i].Clone();
            }

            return components;
        }
    }
}