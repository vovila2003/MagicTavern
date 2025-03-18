using System;
using Sirenix.OdinInspector;
using Tavern.UI.Views;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Tavern.Settings
{
    [Serializable]
    public class GardeningUISettings
    {
        [field: SerializeField]
        public ContainerView SeedItemView { get; private set; }

        [field: SerializeField]
        public ContainerView FertilizerItemView { get; private set; }

        [field: SerializeField]
        public ContainerView MedicineItemView { get; private set; }

        [field: SerializeField]
        public Button MakeSeedsButton { get; private set; }

        [field: SerializeField]
        public PotInfoView PotInfoView { get; private set; }
        
        [field: SerializeField, PreviewField]
        public Sprite EmptyPotSprite { get; private set; }

        [field: SerializeField]
        public ContainerView SeedMakerProductItemsView { get; private set; }

        [field: SerializeField]
        public ContainerView SeedMakerSeedsView { get; private set; }

        [field: SerializeField]
        public ConvertInfoView ConvertInfoView { get; private set; }
    }
}