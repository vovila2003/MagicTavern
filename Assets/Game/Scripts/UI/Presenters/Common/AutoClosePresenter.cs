using System;
using Tavern.InputServices.Interfaces;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class AutoClosePresenter
    {
        public event Action OnClickOutside;
        
        private RectTransform _rectTransform;
        private readonly IMouseClickInput _input;

        public AutoClosePresenter(IMouseClickInput input)
        {
            _input = input;
        }

        public void Enable(IView view)
        {
            _rectTransform = view.RectTransform;
            _input.OnMouseClicked += OnClicked;
        }

        public void Disable()
        {
            _input.OnMouseClicked -= OnClicked;
            _rectTransform = null;
        }

        private void OnClicked(Vector2 position)
        {
            if (IsOutside(_rectTransform, position))
            {
                OnClickOutside?.Invoke();
            }
        }

        private static bool IsOutside(RectTransform panel, Vector2 position) => 
            !RectTransformUtility.RectangleContainsScreenPoint(panel, position);
    }
}