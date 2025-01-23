using JetBrains.Annotations;
using Tavern.Settings;
using Tavern.UI.Presenters;
using UnityEngine;

namespace Tavern.UI.Views
{
    [UsedImplicitly]
    public class ViewsFactory : IViewsFactory
    {
        public IEntityCardViewPool EntityCardViewPool { get; }
        public IItemCardViewPool ItemCardViewPool { get; }
        public IInfoViewProvider InfoViewProvider { get; }

        private readonly UISettings _settings;
        private readonly UISceneSettings _sceneSettings;

        public ViewsFactory(UISettings settings, UISceneSettings sceneSettings)
        {
            _settings = settings;
            _sceneSettings = sceneSettings;
            
            EntityCardViewPool = new EntityCardViewPool(settings, _sceneSettings.Pool);
            ItemCardViewPool = new ItemCardViewPool(settings, _sceneSettings.Pool);
            InfoViewProvider = new InfoViewProvider(CreateInfoPanelView, _sceneSettings.Pool);
        }

        public IEntityCardView GetEntityCardView(Transform viewContentTransform)
        {
            if (EntityCardViewPool.TrySpawnEntityCardViewUnderTransform(
                    viewContentTransform,
                    out IEntityCardView view)) 
                return view;
            
            throw new System.Exception("Failed to get entity card view");
        }

        public IItemCardView GetItemCardView(Transform viewContentTransform)
        {
            if (ItemCardViewPool.TrySpawnItemCardViewUnderTransform(
                    viewContentTransform,
                    out IItemCardView view)) 
                return view;
            
            throw new System.Exception("Failed to get item card view");
        }
        
        public IPanelView CreatePanelView() => 
            Object.Instantiate(_settings.CommonSettings.Panel, _sceneSettings.Canvas);

        public IContainerView CreateLeftGridView(Transform viewContainer) => 
            Object.Instantiate(_settings.CommonSettings.ContainerView, viewContainer);

        public IMatchNewRecipeView CreateMatchNewRecipeView(Transform viewContainer) => 
            Object.Instantiate(_settings.CookingSettings.MatchNewNewRecipeView, viewContainer);

        public IContainerView CreateCookingIngredientsView(Transform viewContainer) => 
            Object.Instantiate(_settings.CookingSettings.CookingIngredientsView, viewContainer);

        public ICookingAndMatchRecipeView CreateCookingAndMatchRecipeView(Transform viewContainer) =>
            Object.Instantiate(_settings.CookingSettings.CookingAndMatchRecipeView, viewContainer);

        public ICookingMiniGameView CreateCookingMiniGameView(Transform viewContainer) => 
            Object.Instantiate(_settings.CookingSettings.CookingMiniGameView, viewContainer);

        public IRecipeIngredientsView CreateRecipeIngredientsView(Transform viewContainer) =>
            Object.Instantiate(_settings.CookingSettings.RecipeIngredientsView, viewContainer);
        
        public IRecipeEffectsView CreateRecipeEffectsView(Transform viewContainer) =>
            Object.Instantiate(_settings.CookingSettings.RecipeEffectsView, viewContainer);
        
        //private
        private InfoPanelView CreateInfoPanelView() =>
            Object.Instantiate(_settings.CommonSettings.InfoPanel, _sceneSettings.Pool);
    }
}