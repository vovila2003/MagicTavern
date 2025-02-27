using System;

namespace Modules.Items
{
    [Flags]
    public enum ItemFlags
    {
        None = 0,
        Stackable = 1,
        Consumable = 2,
        Equippable = 4,
        Effectible = 8,
        Sellable = 16,
    }
}