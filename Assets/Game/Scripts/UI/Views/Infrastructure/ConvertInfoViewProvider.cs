using System;
using Modules.UI;
using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    public sealed class ConvertInfoViewProvider : 
        ViewProvider<IConvertInfoView, ConvertInfoView>, IConvertInfoViewProvider
    {
        public ConvertInfoViewProvider(Func<ConvertInfoView> factory, Transform pool) : base(factory, pool)
        {
        }
    }
}