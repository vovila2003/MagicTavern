using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.GameCycle;
using Modules.GameCycle.Interfaces;
using Tavern.Settings;
using Tavern.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Tavern.Cooking
{
    [UsedImplicitly]
    public class KitchenItemFactory : IInitGameListener, IExitGameListener
    {
        private readonly IObjectResolver _resolver;
        private readonly Dictionary<KitchenItemContext, KitchenItemContextListener> _listeners = new();
        
        private ActiveDishRecipe _dishRecipe;
        private GameCycle _gameCycle;
        private IUiManager _uiManager;
        private SceneSettings _sceneSettings;
        private KitchenItemContext _prefab;

        public IReadOnlyDictionary<KitchenItemContext, KitchenItemContextListener> KitchenItems => _listeners;

        public KitchenItemFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public void Clear()
        {
            foreach ((KitchenItemContext context, KitchenItemContextListener listener) in _listeners)
            {
                listener?.Dispose();
                try
                {
                    Object.Destroy(context?.gameObject);
                }
                catch (Exception _)
                {
                    // ignored
                }
            }
            
            _listeners.Clear();
        }

        public void Create(Vector3 position, Quaternion rotation, KitchenItemConfig config)
        {
            KitchenItemContext context = _resolver.Instantiate(_prefab, position,
                rotation, _sceneSettings.KitchenParent);
            context.Setup(config);
            _listeners.Add(context, new KitchenItemContextListener(context, _dishRecipe, _gameCycle, _uiManager));
        }

        void IInitGameListener.OnInit()
        {
            InitFields();

            foreach (KitchenItemPoint kitchenPoint in _sceneSettings.KitchenPoints)
            {
                Create(kitchenPoint.transform.position, kitchenPoint.transform.rotation, kitchenPoint.Config);
            }

            foreach (KitchenItemPoint point in _sceneSettings.KitchenPoints)
            {
                Object.Destroy(point.gameObject);
            }
        }

        void IExitGameListener.OnExit()
        {
            foreach (KitchenItemContextListener listener in _listeners.Values)
            {
                listener.Dispose();
            }
            
            _listeners.Clear();
        }

        private void InitFields()
        {
            _dishRecipe ??= _resolver.Resolve<ActiveDishRecipe>();
            _gameCycle ??= _resolver.Resolve<GameCycle>();
            _uiManager ??= _resolver.Resolve<IUiManager>();
            _sceneSettings ??= _resolver.Resolve<SceneSettings>();
            _prefab ??= _resolver.Resolve<GameSettings>().CookingSettings.KitchenPrefab;
        }
    }
}