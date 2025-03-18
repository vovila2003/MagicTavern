using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Modules.Items
{
    [Serializable]
    public class NamedConfig : ScriptableObject
    {
        [field: SerializeField, ReadOnly] 
        public string Name { get; protected set; }

        public virtual void OnValidate()
        {
#if UNITY_EDITOR
            string path = AssetDatabase.GetAssetPath(this);
            Name = System.IO.Path.GetFileNameWithoutExtension(path);
#endif
        }
    }
}