using System.Collections.Generic;
using JetBrains.Annotations;
using Modules.GameCycle.Interfaces;
using Tavern.Infrastructure;
using Tavern.Settings;
using Tavern.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Tavern.Cooking
{
    [UsedImplicitly]
    public class KitchenItemFactory : IInitGameListener, IExitGameListener
    {
        private readonly IObjectResolver _resolver;
        private readonly List<KitchenItemContextListener> _listeners = new();
        private readonly ActiveDishRecipe _dishRecipe;
        private readonly GameCycleController _gameCycleController;
        private readonly IUiManager _uiManager;
        private readonly SceneSettings _sceneSettings;
        private readonly KitchenItemContext _prefab;

        public KitchenItemFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
            _dishRecipe = _resolver.Resolve<ActiveDishRecipe>();
            _gameCycleController = _resolver.Resolve<GameCycleController>();
            _uiManager = _resolver.Resolve<IUiManager>();
            _sceneSettings = _resolver.Resolve<SceneSettings>();
            _prefab = _resolver.Resolve<GameSettings>().CookingSettings.KitchenPrefab;
        }

        void IInitGameListener.OnInit()
        {
            
            foreach (KitchenItemPoint kitchenPoint in _sceneSettings.KitchenPoints)
            {
                KitchenItemContext context = _resolver.Instantiate(_prefab, kitchenPoint.transform.position,
                    kitchenPoint.transform.rotation, _sceneSettings.KitchenParent);
                
                context.Setup(kitchenPoint.Config);

                _listeners.Add(new KitchenItemContextListener(
                    context, _dishRecipe, _gameCycleController, _uiManager));
            }

            foreach (KitchenItemPoint point in _sceneSettings.KitchenPoints)
            {
                Object.Destroy(point.gameObject);
            }
        }

        void IExitGameListener.OnExit()
        {
            foreach (KitchenItemContextListener listener in _listeners)
            {
                listener.Dispose();
            }
            
            _listeners.Clear();
        }
    }
}