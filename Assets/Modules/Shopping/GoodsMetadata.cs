using System;
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
        public Sprite Icon;
    }
}