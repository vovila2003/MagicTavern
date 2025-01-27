using System;
using System.Collections.Generic;
using Modules.Items;
using Tavern.Cooking;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public sealed class InfoPresenter
    {
        public event Action<Item> OnAccepted;
        public event Action OnRejected;
        
        private readonly IInfoViewProvider _provider;
        private readonly Transform _parent;
        private readonly AutoClosePresenter _autoClosePresenter;
        
        private Item _item;
        private IInfoPanelView _view;

        public InfoPresenter(IInfoViewProvider provider, Transform parent, PresentersFactory factory)
        {
            _provider = provider;
            _parent = parent;
            _autoClosePresenter = factory.CreateAutoClosePresenter();
        }

        public bool Show(Item item, string command)
        {
            if (item == null) return false;
            
            if (!_provider.TryGetView(_parent, out IInfoPanelView view)) return false;
            
            _item = item;
            _view = view;
            SetupView(command);
            
            _autoClosePresenter.Enable(view);
            _autoClosePresenter.OnClickOutside += OnClose;

            _view.Show();
            
            return true;
        }

        private void SetupView(string command)
        {
            ItemMetadata metadata = _item.ItemMetadata;
            _view.SetTitle(metadata.Title);
            _view.SetDescription(metadata.Description);
            _view.SetIcon(metadata.Icon);
            _view.SetActionButtonText(command);

            SetupEffects();
            
            _view.OnAction += OnAction;
            _view.OnClose += OnClose;
        }

        private void SetupEffects()
        {
            _view.HideAllEffects();
            
            List<IEffectComponent> effects = _item.GetAll<IEffectComponent>();
            int count = Mathf.Min(effects.Count, _view.Effects.Length);
            for (var i = 0; i < count; i++)
            {
                IEffectView viewEffect = _view.Effects[i];
                viewEffect.SetActive(true);
                viewEffect.SetIcon(effects[i].Config.Icon);
            }
        }

        private void OnAction()
        {
            OnAccepted?.Invoke(_item);
            Hide();
        }

        private void OnClose()
        {
            OnRejected?.Invoke();
            Hide();
        }

        private void Hide()
        {
            _view.OnAction -= OnAction;
            _view.OnClose -= OnClose;
            
            _autoClosePresenter.OnClickOutside -= OnClose;
            _autoClosePresenter.Disable();
            
            _view.Hide();
            
            _provider.TryRelease(_view);
            _view = null;
        }
    }
}