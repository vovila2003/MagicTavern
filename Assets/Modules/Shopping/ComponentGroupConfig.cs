using UnityEngine;

namespace Modules.Shopping
{
    [CreateAssetMenu(
        fileName = "ProductGroupConfig",
        menuName = "Settings/Products/Product Group Config")]
    public class ComponentGroupConfig : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }
    }
}