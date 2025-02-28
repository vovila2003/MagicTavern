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

        public KitchenItemFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        void IInitGameListener.OnInit()
        {
            var dishRecipe = _resolver.Resolve<ActiveDishRecipe>();
            var gameCycleController = _resolver.Resolve<GameCycleController>();
            var uiManager = _resolver.Resolve<IUiManager>();
            var sceneSettings = _resolver.Resolve<SceneSettings>();
            KitchenItemContext prefab = _resolver.Resolve<GameSettings>().CookingSettings.KitchenPrefab;
            
            foreach (KitchenItemPoint kitchenPoint in sceneSettings.KitchenPoints)
            {
                KitchenItemContext context = _resolver.Instantiate(prefab, kitchenPoint.transform.position,
                    kitchenPoint.transform.rotation, sceneSettings.KitchenParent);
                
                context.Setup(kitchenPoint.Config);

                _listeners.Add(new KitchenItemContextListener(context, dishRecipe, gameCycleController, uiManager));
            }

            foreach (KitchenItemPoint point in sceneSettings.KitchenPoints)
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