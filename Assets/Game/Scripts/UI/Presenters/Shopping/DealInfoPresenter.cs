using System;
using System.Collections.Generic;
using Modules.Info;
using Modules.Items;
using Tavern.Cooking;
using Tavern.Effects;
using Tavern.Shopping;
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
         private int _price;
        
         public DealInfoPresenter(
             IDealInfoViewProvider provider, 
             Transform parent, 
             CommonPresentersFactory factory)
         {
             _provider = provider;
             _parent = parent;
             _autoClosePresenter = factory.CreateAutoClosePresenter();
         }

         public bool Show(Item item, int maxCount, int price)
         {
             if (item == null) return false;
             
             _item = item;
             _itemConfig = null;

             return Show(item.Metadata, item, maxCount, price);
         }
         
         public bool Show(ItemInfoByConfig itemInfo)
         {
             if (itemInfo == null) return false;
             
             _itemConfig = itemInfo.Item;
             _item = null;

             return Show(_itemConfig.Metadata, _itemConfig, itemInfo.Count, itemInfo.Price);
         }

         private bool Show(Metadata metadata, IHavingComponentsCapable entity,  int maxCount, int price)
         {
             _maxCount = maxCount;
             _price = price;
             
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
             _view.SetPrice($"{_price} р/шт");
             
             SetCurrentValue();

             SubscribeView();
         }

         private void SetupEffects(IHavingComponentsCapable entity)
         {
             _view.HideAllEffects();
             
             List<IEffectComponent> effects = entity.GetAllExtra<IEffectComponent>();
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
             _view.SetSliderValue(_currentCount);
             _view.SetAmount($"{_currentCount}");
             _view.SetTotalPrice($"Всего: {_price * _currentCount} р");
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
             SetCurrentValue();
         }
    }
}