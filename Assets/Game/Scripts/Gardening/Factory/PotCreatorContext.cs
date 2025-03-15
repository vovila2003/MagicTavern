using Sirenix.OdinInspector;
using UnityEngine;
using VContainer;

namespace Tavern.Gardening
{
    public class PotCreatorContext : MonoBehaviour
    {
        private PotFactory _factory;

        [Inject]
        private void Construct(PotFactory factory)
        {
            _factory = factory;
        }

        [Button]
        public void Create(Vector3 position)
        {
            _factory.Create(position, Quaternion.identity);
        }

        [Button]
        public void Destroy(Pot pot)
        {
            _factory.Destroy(pot);
        }
    }
}