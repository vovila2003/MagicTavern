using JetBrains.Annotations;
using Modules.Inventories;
using Tavern.Cooking;
using Tavern.Cooking.MiniGame;
using Tavern.InputServices.Interfaces;
using Tavern.ProductsAndIngredients;
using Tavern.Settings;
using UnityEngine;

namespace Tavern.UI.Presenters
{
    [UsedImplicitly]
    public class CookingPresentersFactory
    {
        private readonly ICommonViewsFactory _commonViewsFactory;
        private readonly ICookingViewsFactory _cookingViewsFactory;
        private readonly DishCookbookContext _dishCookbook;
        private readonly IStackableInventory<PlantProductItem> _plantProductInventory;
        private readonly IStackableInventory<AnimalProductItem> _animalProductInventory;
        private readonly ActiveDishRecipe _activeDishRecipe;
        private readonly DishCrafter _dishCrafter;
        private readonly ISpaceInput _spaceInput;
        private readonly MiniGamePlayer _player;
        private readonly IMouseClickInput _mouseClickInput;
        private readonly UISettings _settings;
        private readonly UISceneSettings _sceneSettings;
        private readonly CookingSettings _cookingSettings;

        public CookingPresentersFactory(
            ICommonViewsFactory commonViewsFactory,
            ICookingViewsFactory cookingViewsFactory, 
            DishCookbookContext dishCookbook,
            IStackableInventory<PlantProductItem> plantProductInventory,
            IStackableInventory<AnimalProductItem> animalProductInventory,
            ActiveDishRecipe activeDishRecipe,
            DishCrafter dishCrafter,
            ISpaceInput spaceInput,
            MiniGamePlayer player,
            IMouseClickInput mouseClickInput,
            UISettings settings,
            UISceneSettings sceneSettings,
            CookingSettings cookingSettings)
        {
            _commonViewsFactory = commonViewsFactory;
            _cookingViewsFactory = cookingViewsFactory;
            _dishCookbook = dishCookbook;
            _plantProductInventory = plantProductInventory;
            _animalProductInventory = animalProductInventory;
            _activeDishRecipe = activeDishRecipe;
            _dishCrafter = dishCrafter;
            _spaceInput = spaceInput;
            _player = player;
            _mouseClickInput = mouseClickInput;
            _settings = settings;
            _sceneSettings = sceneSettings;
            _cookingSettings = cookingSettings;
        }

        public RecipeCardPresenter CreateRecipeCardPresenter(Transform viewContentTransform) =>
            new(_commonViewsFactory.GetEntityCardView(viewContentTransform), 
                _commonViewsFactory.EntityCardViewPool);

        public ItemCardPresenter CreateItemCardPresenter(Transform viewContentTransform) =>
            new(_commonViewsFactory.GetItemCardView(viewContentTransform), 
                _commonViewsFactory.ItemCardViewPool);

        public CookingRecipesPresenter CreateLeftGridPresenter(Transform viewContainer) =>
            new(_commonViewsFactory.CreateLeftGridView(viewContainer), 
                _dishCookbook,
                this, 
                _activeDishRecipe,
                _cookingSettings.EnableRecipeMatching);

        public CookingPanelPresenter CreateCookingPanelPresenter() => 
            new(_commonViewsFactory.CreatePanelView(),
                _dishCrafter,
                this,
                _activeDishRecipe,
                _sceneSettings.Canvas,
                _settings.CommonSettings.SlopsSettings);

        public CookingAndMatchRecipePresenter CreateCookingAndMatchRecipePresenter(
            Transform viewContainer, ActiveDishRecipe recipe) => 
            new(_cookingViewsFactory.CreateCookingAndMatchRecipeView(viewContainer), this);

        public CookingIngredientsPresenter CreateCookingIngredientsPresenter(
            Transform viewContainer, ActiveDishRecipe recipe) =>
            new(_cookingViewsFactory.CreateCookingIngredientsView(viewContainer),
                _plantProductInventory,
                _animalProductInventory,
                this,
                _sceneSettings.Canvas, 
                recipe,
                _cookingSettings.EnableRecipeMatching);

        public MatchNewRecipePresenter CreateMatchNewRecipePresenter(Transform viewContainer) => 
            new(_cookingViewsFactory.CreateMatchNewRecipeView(viewContainer));

        public InfoPresenter CreateInfoPresenter(Transform parent) =>
            new(_commonViewsFactory.InfoViewProvider, parent, this);

        public CookingMiniGamePresenter CreateCookingMiniGamePresenter(Transform parent) =>
            new(_cookingViewsFactory.CreateCookingMiniGameView(parent), 
                _player,
                _spaceInput);

        public RecipeIngredientsPresenter CreateRecipeIngredientsPresenter(Transform parent) => 
            new(_cookingViewsFactory.CreateRecipeIngredientsView(parent), 
                _settings.CookingSettings, 
                CreateInfoPresenter,
                _sceneSettings.Canvas,
                _activeDishRecipe,
                _cookingSettings.EnableRecipeMatching);

        public RecipeEffectsPresenter CreateRecipeEffectsPresenter(Transform parent) => 
            new(_cookingViewsFactory.CreateRecipeEffectsView(parent), 
                _settings.CookingSettings,
                _activeDishRecipe);

        public AutoClosePresenter CreateAutoClosePresenter() => new(_mouseClickInput);
    }
}