using Tavern.UI.Presenters;
using UnityEngine;
using UnityEngine.UI;

namespace Tavern.UI.Views
{
    public class MiniMapView : View, IMiniMapView
    {
        [SerializeField] 
        private Image Map;

        private RectTransform _mapRectTransform;

        public void SetImage(Sprite sprite)
        {
            Map.sprite = sprite;
        }

        public void SetScale(Vector3 factor)
        {
            CheckRectTransform();
            if (_mapRectTransform is null) return;
            
            _mapRectTransform.localScale = factor;
        }

        public void SetPosition(Vector3 position)
        {
            CheckRectTransform();
            if (_mapRectTransform is null) return;
            
            _mapRectTransform.localPosition = position;
        }

        private void CheckRectTransform()
        {
            _mapRectTransform ??= Map.GetComponent<RectTransform>();
        }
    }
}