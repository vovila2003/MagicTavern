using JetBrains.Annotations;
using Tavern.Settings;
using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    [UsedImplicitly]
    public class ViewsFactory : IViewsFactory
    {
        private readonly UISettings _settings;

        public ViewsFactory(UISettings settings)
        {
            _settings = settings;
        }

        public IEntityCardView CreateEntityCardView()
        {
            return Object.Instantiate(_settings.EntityCard, _settings.Canvas);
        }

        public ILeftGridView CreateLeftGridView()
        {
            return Object.Instantiate(_settings.LeftGridView, _settings.Canvas);
        }
    }
}