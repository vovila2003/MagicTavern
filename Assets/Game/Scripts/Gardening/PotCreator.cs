using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Tavern.Gardening
{
    public class PotCreator : MonoBehaviour
    {
        private PotsController _controller;

        [Inject]
        private void Construct(PotsController controller)
        {
            _controller = controller;
        }

        [Button]
        public void Create(Vector3 position)
        {
            _controller.Create(position);
        }

        [Button]
        public void Destroy(Pot pot)
        {
            _controller.Destroy(pot);
        }
    }
}