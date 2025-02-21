using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface IVendorInfoView : IView
    {
        void SetTitle(string text);
        void SetIcon(Sprite sprite);
        void SetMaxStars(int count);
        void SetStars(float stars);
        void SetMoney(string text);
    }
}