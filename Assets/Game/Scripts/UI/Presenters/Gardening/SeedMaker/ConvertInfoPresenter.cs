using System;
using Modules.Info;
using Modules.Items;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    public class ConvertInfoPresenter
    {
         public event Action<Item, int> OnAccepted;
         public event Action OnRejected;
        
         private readonly IConvertInfoViewProvider _provider;
         private readonly Transform _parent;
         private readonly AutoClosePresenter _autoClosePresenter;
        
         private Item _item;
         private IConvertInfoView _view;
         private int _maxCount;
         private int _currentCount;
         private int _ratio;
        
         public ConvertInfoPresenter(
             IConvertInfoViewProvider provider, 
             Transform parent, 
             CommonPresentersFactory factory)
         {
             _provider = provider;
             _parent = parent;
             _autoClosePresenter = factory.CreateAutoClosePresenter();
         }

         public bool Show(Item item, int maxCount, int ratio)
         {
             if (item == null) return false;
             
             _item = item;

             return Show(item.Metadata, maxCount, ratio);
         }
         
         private bool Show(Metadata metadata,  int maxCount, int ratio)
         {
             _maxCount = maxCount;
             _ratio = ratio;
             
             if (!_provider.TryGetView(_parent, out IConvertInfoView view)) return false;
             _view = view;
             _currentCount = 0;
             SetupView(metadata);
             
             _autoClosePresenter.Enable(view);
             _autoClosePresenter.OnClickOutside += OnClose;
        
             _view.Show();
             
             return true;
         }
        
         private void SetupView(Metadata metadata)
         {
             _view.SetTitle(metadata.Title);
             _view.SetIcon(metadata.Icon);
             _view.SetDescription(metadata.Description);
             _view.SetSliderMaxValue(_maxCount);
             _view.SetRatio($"{_ratio}");
             
             SetCurrentValue();

             SubscribeView();
         }

         private void OnAction()
         {
             if (_currentCount == 0) return;
             
             if (_item is not null)
             {
                 OnAccepted?.Invoke(_item, _currentCount);
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
             _view.SetTotalCount($"{_ratio * _currentCount}");
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