using System;
using UnityEngine;

namespace Modules.Items
{
    [Serializable]
    public sealed class ItemMetadata
    {
        [SerializeField]
        public string Title;

        [SerializeField, TextArea] 
        public string Description;

        [SerializeField] 
        public Sprite Icon;
    }
}