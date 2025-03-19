using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    public sealed class HudView : View, IHudView
    {
        [SerializeField]
        private MiniMapView MiniMap;

        public IMiniMapView MiniMapView => MiniMap;
    }
}