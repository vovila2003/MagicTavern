using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.Gardening
{
    [Serializable]
    public class Metadata
    {
        [SerializeField]
        public string Title;

        [SerializeField, TextArea] 
        public string Description;

        [SerializeField, PreviewField] 
        public Sprite Icon;
        
    }
}