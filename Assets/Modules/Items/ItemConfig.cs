using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Modules.Items
{
    public abstract class ItemConfig : NamedConfig
    {
        [field: SerializeField] 
        public ItemFlags Flags { get; private set; }

        [SerializeReference] 
        protected List<IItemComponent> Components;

        [field: SerializeField]
        public Metadata Metadata { get; private set; }

        public abstract Item Create();

        public List<T> GetAll<T>()
        {
            var result = new List<T>();
            foreach (IItemComponent attribute in Components)
            {
                if (attribute is T tAttribute)
                {
                    result.Add(tAttribute);
                }
            }
            
            return result;
        }

        protected void SetFlags(ItemFlags flags) => Flags |= flags;

        protected void ResetFlags(ItemFlags flags) => Flags &= ~flags;

        protected void SetName(string newName) => Name = newName;

        protected bool TryGet<T>(out T component)
        {
            foreach (IItemComponent attribute in Components)
            {
                if (attribute is not T tAttribute) continue;
                
                component = tAttribute;
                return true;
            }

            component = default;
            return false;
        }

        protected bool Has<T>() => Components.OfType<T>().Any();

        protected virtual void Awake()
        {
            Components ??= new List<IItemComponent>();
        }

        protected IItemComponent[] GetComponentClones()
        {
            int count = Components.Count;
            var components = new IItemComponent[count];

            for (var i = 0; i < count; i++)
            {
                components[i] = Components[i].Clone();
            }

            return components;
        }
    }
}