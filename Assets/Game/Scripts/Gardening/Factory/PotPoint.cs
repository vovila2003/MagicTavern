using UnityEngine;

namespace Tavern.Gardening
{
    public class PotPoint : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 0.8f);
            Gizmos.color = Color.white;
        }
    }
}