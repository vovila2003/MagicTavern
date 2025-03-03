using UnityEngine;
using UnityEngine.Events;

namespace Tavern.UI.Presenters
{
    public interface IPotInfoView : IView
    {
        event UnityAction OnWateringClicked;
        event UnityAction OnGatherClicked;
        void SetTitle(string title);
        void SetIcon(Sprite icon);
        void SetProgress(float progress);
        void SetIsFertilized(bool isFertilized);
        void SetIsSick(bool isSicked);
        void SetIsWaterNeed(bool isWaterNeed);
    }
}