using System;
using UnityEngine;

namespace Tavern.Gardening.Medicine
{
    [Serializable]
    public class Component_Test : ICloneable
    {
        [SerializeField] 
        private string LogText;

        public string Text
        {
            get => LogText;
            private set => LogText = value;
        }

        public Component_Test(string text = null)
        {
            Text = text;
        }

        object ICloneable.Clone() => new Component_Test(LogText);
    }
}