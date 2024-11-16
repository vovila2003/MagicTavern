using System;
using Modules.Items;
using UnityEngine;

namespace Modules.Looting
{
    // [Serializable]
    // public class LootItem : Item
    // {
    //     [SerializeField]
    //     private KitchenItemType KitchenItemType;
    //     
    //     public KitchenItemType Type => KitchenItemType;
    //     
    //     public KitchenItem(KitchenItemType type, string name, ItemFlags flags, ItemMetadata metadata, params object[] attributes) 
    //         : base(name, flags, metadata, attributes)
    //     {
    //         KitchenItemType = type;
    //     }
    //     
    //     public new virtual KitchenItem Clone()
    //     {
    //         object[] attributes = GetAttributes();
    //
    //         return new KitchenItem(KitchenItemType, Name, Flags, Metadata, attributes);
    //     }
    // }
}