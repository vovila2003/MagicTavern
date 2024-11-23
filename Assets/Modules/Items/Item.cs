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
        public List<object> Components;
        
        public string ItemName => Name;
        public ItemFlags ItemFlags => Flags;
        public ItemMetadata ItemMetadata => Metadata;

        public Item(
            string name,
            ItemFlags flags,
            ItemMetadata metadata,
            params object[] attributes)
        {
            Name = name;
            Flags = flags;
            Metadata = metadata;
            Components = new List<object>(attributes);
        }

        public void SetFlags(ItemFlags flags) => Flags |= flags;
        public void ResetFlags(ItemFlags flags) => Flags &= ~flags;

        public T GetComponent<T>()
        {
            foreach (object attribute in Components)
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
            object[] attributes = GetAttributes();

            return new Item(Name, Flags, Metadata, attributes);
        }

        protected object[] GetAttributes()
        {
            int count = Components.Count;
            var attributes = new object[count];

            for (var i = 0; i < count; i++)
            {
                object attribute = Components[i];
                if (attribute is ICloneable cloneable)
                {
                    attribute = cloneable.Clone();
                }

                attributes[i] = attribute;
            }

            return attributes;
        }
    }
}