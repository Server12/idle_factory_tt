using System;
using System.Collections.Generic;
using Game.Configs.UI;
using Game.Data;
using Game.Map.Data;
using Game.Map.Models;
using Game.Player.Models;
using Game.UI.Components;
using Game.UI.Managers;
using Game.UI.Popups;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Game.UI.Presenters
{
    public class CraftToolsPopupPresenter : IPopupPresenter<ICraftBuildingModel>, IInitializable, IDisposable
    {
        private readonly IUIFactory _factory;
        private readonly ICraftBuildingModel _model;
        private readonly InventoryModel _inventoryModel;

        private CraftPopupView _popupView;

        private List<RecipePanel> _recipePanels;

        public CraftToolsPopupPresenter(IUIFactory factory, ICraftBuildingModel model,
            InventoryModel inventoryModel)
        {
            _factory = factory;
            _model = model;
            _inventoryModel = inventoryModel;
            _recipePanels = new List<RecipePanel>();
        }

        public void OpenPopup(ICraftBuildingModel model, Action completeCallback = null)
        {
            if (_popupView == null)
            {
                _popupView = _factory.GetOrCreatePopup<CraftPopupView>();
                _popupView.startButton.OnClick += OnStartStopClickedHandler;
                _popupView.panelCloseButton.OnClick += OnCloseClickHandler;
                _popupView.startButton.Label = "Start";
                var recipePanelInstance = _popupView.RecipePanelInstance;
                var parent = _popupView.RecipePanelInstance.transform.parent;
                recipePanelInstance.gameObject.SetActive(false);
                foreach (var modelRecipe in _model.Recipes)
                {
                    var recipePanel = Object.Instantiate(recipePanelInstance, parent);
                    recipePanel.SetCraftData(modelRecipe);
                    recipePanel.checkMark.enabled = false;
                    recipePanel.OnPanelClicked += OnRecipePanelClicked;
                    recipePanel.gameObject.SetActive(true);
                    _recipePanels.Add(recipePanel);
                }
            }

          

            _popupView.Open();

            if (_model.CurrentRecipe.CraftedItem != ResourceItemType.None)
            {
                _inventoryModel.OnChanged -= OnInventoryChangedHandler;
                _inventoryModel.OnChanged += OnInventoryChangedHandler;
                OnInventoryChangedHandler(default);
            }
        }

        private void OnRecipePanelClicked(RecipePanel recipePanel)
        {
            foreach (var panel in _recipePanels)
            {
                if (panel != recipePanel)
                {
                    panel.checkMark.enabled = false;
                }
            }

            _model.SelectCraftRecipe(recipePanel.CraftData);
        }

        private void OnRecipeChangedHandler()
        {
            _inventoryModel.OnChanged -= OnInventoryChangedHandler;
            _inventoryModel.OnChanged += OnInventoryChangedHandler;
            _popupView.CraftPanel.SetCraftData(_model.CurrentRecipe);
            OnInventoryChangedHandler(default);
        }

        private void OnInventoryChangedHandler(InventoryChangedEventData eventData)
        {
            var left = _inventoryModel.Get(_model.CurrentRecipe.Item1);
            var right = _inventoryModel.Get(_model.CurrentRecipe.Item2);
            var result = _inventoryModel.Get(_model.CurrentRecipe.CraftedItem);
            _popupView.CraftPanel.UpdateCounters(left, right, result);
        }

        private void OnProductionStateChanged(IProductFactoryBuildingModel model)
        {
            if (model.CurrentState == BuildingProductionState.Started)
            {
                _popupView.startButton.Label = "Stop";
            }
            else if (model.CurrentState == BuildingProductionState.Stopped)
            {
                _popupView.startButton.Label = "Start";
            }
        }

        private void OnStartStopClickedHandler()
        {
            if (_model.CurrentState != BuildingProductionState.Started)
            {
                _model.Start();
            }
            else
            {
                _model.Stop();
            }
        }

        private void OnCloseClickHandler()
        {
            _inventoryModel.OnChanged -= OnInventoryChangedHandler;
        }


        public void Dispose()
        {
            _inventoryModel.OnChanged -= OnInventoryChangedHandler;
            _model.OnRecipeChanged -= OnRecipeChangedHandler;
            _model.OnProductionStateChange -= OnProductionStateChanged;

            if (_popupView != null)
            {
                foreach (var recipePanel in _recipePanels)
                {
                    recipePanel.OnPanelClicked -= OnRecipePanelClicked;
                }

                _popupView.startButton.OnClick -= OnStartStopClickedHandler;
                _popupView.panelCloseButton.OnClick -= OnCloseClickHandler;
            }
        }

        public void Initialize()
        {
            _model.OnRecipeChanged += OnRecipeChangedHandler;
            _model.OnProductionStateChange += OnProductionStateChanged;
        }
    }
}