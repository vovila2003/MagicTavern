using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    public sealed class InfoViewProvider : ViewProvider<IInfoPanelView, InfoPanelView>, IInfoViewProvider
    {
        public InfoViewProvider(InfoPanelView view, Transform pool) : base(view, pool)
        {
        }
    }
}