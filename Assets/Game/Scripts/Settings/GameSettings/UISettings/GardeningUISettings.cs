using System;
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
    }
}