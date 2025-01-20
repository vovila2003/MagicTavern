using UnityEngine;

namespace Tavern.UI.Views
{
    public abstract class ViewProvider<TI, T> where T : Component, TI
    {
        private readonly T _view;
        private readonly Transform _pool;
        private bool _isBusy;

        protected ViewProvider(T view, Transform pool)
        {
            _view = view;
            _pool = pool;
            _isBusy = false;
        }

        public bool TryGetView(Transform parent, out TI view)
        {
            if (_isBusy)
            {
                view = default;
                return false;
            }
            
            _view.transform.SetParent(parent);
            _isBusy = true;
            view = _view;

            return true;
        }

        public bool TryRelease(TI _)
        {
            if (!_isBusy) return false;
            
            _view.transform.SetParent(_pool);
            _isBusy = false;
            
            return true;
        }
    }
}