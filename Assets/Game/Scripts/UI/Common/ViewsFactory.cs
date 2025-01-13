using JetBrains.Annotations;
using Tavern.Settings;
using UnityEngine;

namespace Tavern.UI
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
            EntityCardView entityCardView = Object.Instantiate(_settings.EntityCard, _settings.Canvas);
            //entityCardView.gameObject.SetActive(false);
            return entityCardView;
        }

        public LeftGridView CreateLeftGridView()
        {
            LeftGridView leftGridView = Object.Instantiate(_settings.LeftGridView, _settings.Canvas);
            //leftGridView.gameObject.SetActive(false);
            return leftGridView;
        }
    }
}