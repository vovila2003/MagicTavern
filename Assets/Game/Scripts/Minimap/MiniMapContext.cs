using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Tavern.Minimap
{
    public class MiniMapContext : MonoBehaviour
    {
        private MinimapService _mapService;
        
        [Inject]
        private void Construct(MinimapService mapService)
        {
            _mapService = mapService;
        }

        [Button]
        public void ChangePosition(Vector3 position)
        {
            _mapService.ChangePosition(position);
        }
    }
}