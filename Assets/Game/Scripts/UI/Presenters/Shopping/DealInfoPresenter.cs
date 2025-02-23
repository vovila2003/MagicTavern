using System;
using System.Collections.Generic;
using Modules.Info;
using Modules.Items;
using Tavern.Cooking;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public sealed class DealInfoPresenter
    {
         private const string FromChef = "От Шефа!";
         public event Action<Item, int> OnAccepted;
         public event Action<ItemConfig, int> OnConfigAccepted;
         public event Action OnRejected;
        
         private readonly IDealInfoViewProvider _provider;
         private readonly Transform _parent;
         private readonly AutoClosePresenter _autoClosePresenter;
        
         private Item _item;
         private ItemConfig _itemConfig;
         private IDealInfoView _view;
         private int _maxCount;
         private int _currentCount;
        
         public DealInfoPresenter(IDealInfoViewProvider provider, Transform parent, CommonPresentersFactory factory)
         {
             _provider = provider;
             _parent = parent;
             _autoClosePresenter = factory.CreateAutoClosePresenter();
         }
        
         public bool Show(Item item, int maxCount)
         {
             if (item == null) return false;
             
             _item = item;
             _itemConfig = null;
             _maxCount = maxCount;

             return Show(item.Metadata, item);
         }
         
         public bool Show(ItemConfig itemConfig, int maxCount)
         {
             if (itemConfig == null) return false;
             
             _itemConfig = itemConfig;
             _item = null;
             _maxCount = maxCount;

             return Show(itemConfig.Metadata, itemConfig);
         }

         private bool Show(Metadata metadata, IHavingComponentsCapable entity)
         {
             if (!_provider.TryGetView(_parent, out IDealInfoView view)) return false;
             _view = view;
             _currentCount = 0;
             SetupView(metadata, entity);
             
             _autoClosePresenter.Enable(view);
             _autoClosePresenter.OnClickOutside += OnClose;
        
             _view.Show();
             
             return true;
         }
        
         private void SetupView(Metadata metadata, IHavingComponentsCapable entity)
         {
             _view.SetTitle(metadata.Title);
             _view.SetIcon(metadata.Icon);
             _view.SetExtra(false);
             string description = metadata.Description;
        
             SetupEffects(entity);
        
             if (entity.Has<ComponentDishExtra>())
             {
                 _view.SetExtra(true);
                 description = $"{FromChef} {description}";
             }
        
             _view.SetDescription(description);
             _view.SetSliderMaxValue(_maxCount);
             
             SetCurrentValue();

             SubscribeView();
         }

         private void SetupEffects(IHavingComponentsCapable entity)
         {
             _view.HideAllEffects();
             
             List<IEffectComponent> effects = entity.GetAll<IEffectComponent>();
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
             if (_currentCount == 0) return;
             
             if (_item is not null)
             {
                 OnAccepted?.Invoke(_item, _currentCount);
             }
             
             if (_itemConfig is not null)
             {
                 OnConfigAccepted?.Invoke(_itemConfig, _currentCount);
             }
             
             Hide();
         }

         private void OnClose()
         {
             OnRejected?.Invoke();
             Hide();
         }

         private void Hide()
         {
             UnsubscribeView();

             _autoClosePresenter.OnClickOutside -= OnClose;
             _autoClosePresenter.Disable();
             
             _view.Hide();
             
             _provider.TryRelease(_view);
             _view = null;
             _item = null;
             _itemConfig = null;
         }

         private void SubscribeView()
         {
             _view.OnAction += OnAction;
             _view.OnClose += OnClose;
             _view.OnPlus += OnPlus;
             _view.OnMinus += OnMinus;
             _view.OnMax += OnMax;
             _view.OnMin += OnMin;
             _view.OnSliderChanged += OnSliderChanged;
         }

         private void UnsubscribeView()
         {
             _view.OnAction -= OnAction;
             _view.OnClose -= OnClose;
             _view.OnPlus -= OnPlus;
             _view.OnMinus -= OnMinus;
             _view.OnMax -= OnMax;
             _view.OnMin -= OnMin;
             _view.OnSliderChanged -= OnSliderChanged;
         }

         private void SetCurrentValue()
         {
             _view.SetAmount($"{_currentCount}");
             _view.SetSliderValue(_currentCount);
         }

         private void OnPlus(int value)
         {
             _currentCount = Mathf.Clamp(_currentCount + value, 0, _maxCount);
             SetCurrentValue();
         }

         private void OnMinus(int value) => OnPlus(-value);

         private void OnMax()
         {
             _currentCount = _maxCount;
             SetCurrentValue();
         }

         private void OnMin()
         {
             _currentCount = 1;
             SetCurrentValue();
         }

         private void OnSliderChanged(float value)
         {
             _currentCount = (int) value;
             _view.SetAmount($"{_currentCount}");
         }
    }
}