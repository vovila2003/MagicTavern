using System;
using JetBrains.Annotations;
using Tavern.InputServices.Interfaces;
using UnityEngine;
using VContainer.Unity;

namespace Tavern.InputServices
{
    [UsedImplicitly]
    public sealed class MouseClickInputService :
        IMouseClickInput,
        ITickable
    {
        public event Action<Vector2> OnMouseClicked;

        void ITickable.Tick()
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
            {
                OnMouseClicked?.Invoke(Input.mousePosition);
            }
        }
    }
}