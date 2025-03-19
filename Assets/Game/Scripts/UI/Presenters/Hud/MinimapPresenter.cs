using Tavern.Minimap;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class MinimapPresenter : BasePresenter
    {
        private readonly MinimapService _minimapService;
        private readonly IMiniMapView _view;
        
        public MinimapPresenter(
            IMiniMapView view, 
            MinimapService minimapService
            ) : base(view)
        {
            _view = view;
            _minimapService = minimapService;
        }

        protected override void OnShow()
        {
            _view.SetImage(_minimapService.Minimap);
            _view.SetScale(_minimapService.Scale);
            
            _minimapService.OnPositionChanged += OnPositionChanged;
        }

        protected override void OnHide()
        {
            _minimapService.OnPositionChanged -= OnPositionChanged;
        }

        private void OnPositionChanged(Vector3 position)
        {
            _view.SetPosition(position);
        }
    }
}