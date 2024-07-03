using System;
using Game.Configs.UI;
using Game.Data;
using Game.Map.Controllers.Factory;
using Game.Map.Models;
using Game.Map.Views.Buildings;
using Game.UI.Presenters;
using VContainer.Unity;

namespace Game.Map
{
    public class MarketBuildingPresenter : IStartable, IDisposable
    {
        private readonly MapBuildingsFactory _factory;
        private readonly MarketBuildingModel _model;
        private readonly UIIconsSO _iconsSo;
        private readonly IPopupPresenter<IMarketBuildingModel> _popupPresenter;

        private BuildingView _buildingView;

        public MarketBuildingPresenter(MapBuildingsFactory factory, MarketBuildingModel model, UIIconsSO iconsSo,
            IPopupPresenter<IMarketBuildingModel> popupPresenter)
        {
            _factory = factory;
            _model = model;
            _iconsSo = iconsSo;
            _popupPresenter = popupPresenter;
        }

        public void Start()
        {
            _buildingView = _factory.Create(_model);
            _buildingView.HudMarker.SetIcon(_iconsSo.GetCurrencyIcon(CurrencyType.Gold));
            _buildingView.OnClicked += OnMarketClickedHandler;
        }

        private void OnMarketClickedHandler(int viewIndex)
        {
            _popupPresenter.OpenPopup(_model);
        }

        public void Dispose()
        {
            if (_buildingView != null)
            {
                _buildingView.OnClicked -= OnMarketClickedHandler;
            }
        }
    }
}