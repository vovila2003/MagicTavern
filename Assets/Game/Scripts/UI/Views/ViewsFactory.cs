using JetBrains.Annotations;
using Tavern.Settings;
using UnityEngine;

namespace Tavern.UI.Views
{
    [UsedImplicitly]
    public class ViewsFactory
    {
        private readonly UISettings _settings;

        public ViewsFactory(UISettings settings)
        {
            _settings = settings;
        }

        public EntityCardView CreateEntityCardView()
        {
            return Object.Instantiate(_settings.EntityCard, _settings.Canvas);
        }

        public LeftGridView CreateLeftGridView()
        {
            return Object.Instantiate(_settings.LeftGridView, _settings.Canvas);
        }
    }
}