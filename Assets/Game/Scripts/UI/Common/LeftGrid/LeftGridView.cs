using UnityEngine;

namespace Tavern.UI
{
    public class LeftGridView : MonoBehaviour
    {
        [SerializeField] 
        private Transform Content;
        
        public void AddCardView(EntityCardView card)
        {
            card.transform.SetParent(Content);
        }
    }
}