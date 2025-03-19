using System;
using Sirenix.OdinInspector;
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

        [Button]
        public void SetScale(float factor)
        {
            CheckRectTransform();
            if (_mapRectTransform is null) return;
            
            _mapRectTransform.localScale = new Vector3(factor, factor, factor);
        }

        [Button]
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