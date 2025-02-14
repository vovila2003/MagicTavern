using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Modules.Items
{
    public abstract class ItemConfig : ScriptableObject
    {
        [ShowInInspector, ReadOnly] 
        public string Name { get; private set; }

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

            if (Name != string.Empty) return;
        }

        protected virtual void OnValidate()
        {
#if UNITY_EDITOR
            string path = AssetDatabase.GetAssetPath(this);
            Name = System.IO.Path.GetFileNameWithoutExtension(path);
#endif
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