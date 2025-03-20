using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface IMiniMapView : IView
    {
        void SetImage(Sprite sprite);
        void SetScale(Vector3 factor);
        void SetPosition(Vector3 position);
    }
}