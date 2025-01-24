using System;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface IRecipeIngredientView
    {
        event Action<IRecipeIngredientView> OnRightClicked;
        event Action<IRecipeIngredientView> OnLeftClicked;
        void SetTitle(string title);
        void SetBackgroundColor(Color color);
        void SetIcon(Sprite icon);
        void SetFake(bool fake);
    }
}