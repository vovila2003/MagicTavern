using UnityEngine;

namespace Tavern.UI.Presenters
{
    public interface IEffectView
    {
        void SetIcon(Sprite icon);
        void SetActive(bool active);
        GameObject GetGameObject();
    }
}