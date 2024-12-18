using Sirenix.OdinInspector;
using UnityEngine;

namespace Tavern.Storages
{
    public class SeedsStorageContext : MonoBehaviour
    {
        [SerializeField] 
        private bool DebugMode;

        [SerializeField, ShowIf("DebugMode")] 
        private int StartValueInStorageInDebugMode;
        
        [SerializeField]
        private SeedsStorage Storages;

        private void Start()
        {
            if (!DebugMode) return;

            foreach (PlantStorage storage in Storages.PlantStorages)
            {
                storage.Add(StartValueInStorageInDebugMode);
            }
        }
    }
}