using System;
using Modules.UI;
using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    public sealed class InfoViewProvider : ViewProvider<IInfoPanelView, InfoPanelView>, IInfoViewProvider
    {
        public InfoViewProvider(Func<InfoPanelView> factory, Transform pool) : base(factory, pool)
        {
        }
    }
}