using System;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface IItemCardView : IView
    {
        event Action OnLeftClicked;
        event Action OnRightClicked;
        void SetIcon(Sprite icon);
        void SetParent(Transform parent);
        void SetCount(string count);
    }
}