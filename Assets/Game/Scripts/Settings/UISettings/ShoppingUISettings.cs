using System;
using Tavern.UI.Views;
using UnityEngine;

namespace Tavern.Settings
{
    [Serializable]
    public class ShoppingUISettings
    {
        [field: SerializeField]
        public CategoriesView CategoriesView { get; private set; }

        [field: SerializeField]
        public ContainerView ShopItemView { get; private set; }

        [field: SerializeField]
        public VendorInfoView VendorInfoView { get; private set; }

        [field: SerializeField]
        public ContainerView CharacterItemView { get; private set; }

        [field: SerializeField]
        public CharacterInfoView CharacterInfoView { get; private set; }

        [field: SerializeField]
        public FilterView FilterView { get; private set; }
        
        [field: SerializeField]
        public DealInfoView DealInfoView { get; private set; }
    }
}