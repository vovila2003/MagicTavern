using Modules.Items;
using UnityEngine;

namespace Tavern.Gardening.Medicine
{
    [CreateAssetMenu(
        fileName = "MedicineItemsCatalog", 
        menuName = "Settings/Gardening/Medicine/Medicine Items Catalog")]
    public class MedicineItemsCatalog : ItemsCatalog<MedicineItem>
    {
    }
}