using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Shopping
{
    [Serializable]
    public sealed class GoodsMetadata
    {
        [SerializeField]
        public string Title;

        [SerializeField, TextArea] 
        public string Description;

        [SerializeField] 
        [PreviewField(50, ObjectFieldAlignment.Right)]
        public Sprite Icon;
    }
}