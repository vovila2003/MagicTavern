using System;
using System.Collections.Generic;
using Modules.Info;
using Modules.Items;
using Tavern.Cooking;
using Tavern.Effects;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    
    public sealed class InfoPresenter
    {
        public enum Mode
        {
            Dialog,
            Info
        }
        
        private const string FromChef = "От Шефа!";
        public event Action<Item> OnAccepted;
        public event Action OnRejected;
        
        private readonly IInfoViewProvider _provider;
        private readonly Transform _parent;
        private readonly AutoClosePresenter _autoClosePresenter;
        
        private Item _item;
        private IInfoPanelView _view;

        public InfoPresenter(IInfoViewProvider provider, Transform parent, CommonPresentersFactory factory)
        {
            _provider = provider;
            _parent = parent;
            _autoClosePresenter = factory.CreateAutoClosePresenter();
        }

        public bool Show(Item item, Mode mode, string command, string additionDescription = null)
        {
            if (item == null) return false;
            
            if (!_provider.TryGetView(_parent, out IInfoPanelView view)) return false;
            
            _item = item;
            _view = view;
            SetupView(mode, command, additionDescription);
            
            _autoClosePresenter.Enable(view);
            _autoClosePresenter.OnClickOutside += OnClose;

            _view.Show();
            
            return true;
        }

        private void SetupView(Mode mode, string command, string additionDescription = null)
        {
            Metadata metadata = _item.Metadata;
            _view.SetTitle(metadata.Title);
            _view.SetIcon(metadata.Icon);
            _view.SetMode(mode);
            _view.SetActionButtonText(command);
            _view.SetExtra(false);
            string description = additionDescription is not null ? 
                $"{additionDescription}\n{_item.Config.Description}" 
                : _item.Config.Description;

            SetupEffects();

            if (_item.Has<ComponentDishExtra>())
            {
                _view.SetExtra(true);
                description = $"{FromChef} {description}";
            }

            _view.SetDescription(description);

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