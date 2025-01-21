using System;
using UnityEngine;

namespace Modules.UI
{
    public abstract class ViewProvider<TI, T> where T : Component, TI
    {
        private readonly Func<T> _factory;
        private readonly Transform _pool;
        private T _view;
        private bool _isBusy;

        protected ViewProvider(Func<T> viewFactory, Transform pool)
        {
            _factory = viewFactory;
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

            _view ??= _factory();
            
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