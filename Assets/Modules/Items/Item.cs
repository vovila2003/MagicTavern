using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Modules.Items
{
    [Serializable]
    public class Item
    {
        [SerializeField]
        protected string Name;

        [SerializeField]
        protected ItemFlags Flags;
        
        [SerializeField] 
        protected ItemMetadata Metadata;
        
        [SerializeReference] 
        public List<IItemComponent> Components;
        
        public string ItemName => Name;
        public ItemFlags ItemFlags => Flags;
        public ItemMetadata ItemMetadata => Metadata;

        public Item(
            string name,
            ItemFlags flags,
            ItemMetadata metadata,
            params IItemComponent[] attributes)
        {
            Name = name;
            Flags = flags;
            Metadata = metadata;
            Components = new List<IItemComponent>(attributes);
        }

        public void SetFlags(ItemFlags flags) => Flags |= flags;
        public void ResetFlags(ItemFlags flags) => Flags &= ~flags;

        public T GetComponent<T>()
        {
            foreach (IItemComponent attribute in Components)
            {
                if (attribute is T tAttribute)
                {
                    return tAttribute;
                }
            }

            throw new Exception($"Attribute of type {typeof(T).Name} is not found!");
        }

        public bool HasComponent<T>() => Components.OfType<T>().Any();

        public virtual Item Clone()
        {
            IItemComponent[] components = GetComponents();

            return new Item(Name, Flags, Metadata, components);
        }

        protected IItemComponent[] GetComponents()
        {
            int count = Components.Count;
            var components = new IItemComponent[count];

            for (var i = 0; i < count; i++)
            {
                IItemComponent component = Components[i];
                if (component is IItemComponent cloneable)
                {
                    component = cloneable.Clone();
                }

                components[i] = component;
            }

            return components;
        }
    }
}