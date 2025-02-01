using Modules.Items;
using UnityEngine;

namespace Tavern.Storages
{
    [CreateAssetMenu(
        fileName = "ResourceConfig",
        menuName = "Settings/Resources/Resource Config")]
    public class ResourceItemConfig : ItemConfig<Item>
    {
    }
}