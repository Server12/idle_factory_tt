using System;
using Game.Configs.UI;
using Game.Data;
using Game.Map.Controllers.Factory;
using Game.Map.Data;
using Game.Map.Models;
using Game.Map.Views.Buildings;
using Game.UI.Presenters;
using VContainer.Unity;

namespace Game.Map
{
    public class ToolsFactoryBuildingPresenter : IStartable, IDisposable
    {
        private readonly MapBuildingsFactory _factory;
        private readonly ToolsFactoryBuildingModel _model;
        private readonly UIIconsSO _icons;
        private readonly IPopupPresenter<ICraftBuildingModel> _popupPresenter;

        private BuildingView _toolsFactoryBuilding;

        public ToolsFactoryBuildingPresenter(MapBuildingsFactory factory, ToolsFactoryBuildingModel model,
            UIIconsSO icons, IPopupPresenter<ICraftBuildingModel> popupPresenter)
        {
            _factory = factory;
            _model = model;
            _icons = icons;
            _popupPresenter = popupPresenter;
        }

        public void Start()
        {
            _model.OnRecipeChanged += OnRecipeChangedHandler;
            _model.OnProductionStateChange += OnProductionChangedHandler;

            _toolsFactoryBuilding = _factory.Create(_model);
            _toolsFactoryBuilding.OnClicked += OnBuildingClickedHandler;
            _toolsFactoryBuilding.HudMarker.SetIcon(_icons.GetResourceItemIcon(ResourceItemType.None));
            _toolsFactoryBuilding.HudMarker.disabledIcon.gameObject.SetActive(true);
        }

        private void OnProductionChangedHandler(IProductFactoryBuildingModel model)
        {
            _toolsFactoryBuilding.HudMarker.SetIcon(_icons.GetResourceItemIcon(_model.ProductType));
            _toolsFactoryBuilding.HudMarker.disabledIcon.gameObject.SetActive(model.CurrentState !=
                                                                              BuildingProductionState.Started);
        }

        private void OnRecipeChangedHandler()
        {
            _toolsFactoryBuilding.HudMarker.SetIcon(_icons.GetResourceItemIcon(_model.ProductType));
            _toolsFactoryBuilding.HudMarker.disabledIcon.gameObject.SetActive(_model.CurrentState !=
                                                                              BuildingProductionState.Started);
        }

        private void OnBuildingClickedHandler(int viewIndex)
        {
            _popupPresenter.OpenPopup(_model, () => { });
        }

        public void Dispose()
        {
            _model.OnRecipeChanged -= OnRecipeChangedHandler;
            _model.OnProductionStateChange -= OnProductionChangedHandler;

            if (_toolsFactoryBuilding != null)
            {
                _toolsFactoryBuilding.OnClicked -= OnBuildingClickedHandler;
            }
        }
    }
}