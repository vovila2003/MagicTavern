using System;
using UnityEngine;

namespace Modules.Gardening
{
    [Serializable]
    public sealed class Metadata
    {
        [SerializeField]
        public string Title;

        [SerializeField, TextArea] 
        public string Description;

        [SerializeField] 
        public Sprite Icon;
        
        
    }
}