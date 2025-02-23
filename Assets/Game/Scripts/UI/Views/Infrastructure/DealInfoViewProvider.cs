using System;
using Modules.UI;
using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    public sealed class DealInfoViewProvider : ViewProvider<IDealInfoView, DealInfoView>, IDealInfoViewProvider
    {
        public DealInfoViewProvider(Func<DealInfoView> factory, Transform pool) : base(factory, pool)
        {
        }
    }
}