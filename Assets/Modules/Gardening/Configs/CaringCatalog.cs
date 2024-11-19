using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Gardening
{
    [CreateAssetMenu(
        fileName = "CaringCatalog", 
        menuName = "Settings/Gardening/Caring Catalog")]
    public class CaringCatalog : ScriptableObject
    {
        [SerializeField] 
        private Caring[] Carings;
        
        private readonly Dictionary<string, Caring> _caringDict = new();

        public bool TryGetCaring(string caringName, out Caring caring) => 
            _caringDict.TryGetValue(caringName, out caring);

        private void OnValidate()
        {
            var collection = new Dictionary<string, bool>();
            foreach (Caring settings in Carings)
            {
                string caringName = settings.CaringName;
                _caringDict[caringName] = settings;
                if (collection.TryAdd(caringName, true))
                {
                    continue;
                }

                throw new Exception($"Duplicate caring {caringName} in catalog");
            }            
        }
    }
}