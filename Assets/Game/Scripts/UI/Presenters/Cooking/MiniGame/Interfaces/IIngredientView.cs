using System;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface IIngredientView
    {
        event Action<IIngredientView> OnRightClicked;
        event Action<IIngredientView> OnLeftClicked;
        void SetTitle(string title);
        void SetBackgroundColor(Color color);
        void SetIcon(Sprite icon);
    }
}