using System;
using UnityEngine;
using UnityEngine.Serialization;

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
        public object[] Attributes;
        
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
            Attributes = attributes;
        }

        public T GetAttribute<T>()
        {
            foreach (object attribute in Attributes)
            {
                if (attribute is T tAttribute)
                {
                    return tAttribute;
                }
            }

            throw new Exception($"Attribute of type {typeof(T).Name} is not found!");
        }

        public virtual Item Clone()
        {
            object[] attributes = GetAttributes();

            return new Item(Name, Flags, Metadata, attributes);
        }

        protected object[] GetAttributes()
        {
            int count = Attributes.Length;
            var attributes = new object[count];

            for (var i = 0; i < count; i++)
            {
                object attribute = Attributes[i];
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