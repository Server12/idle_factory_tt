using System;
using System.Collections.Generic;
using Game.Configs.UI;
using Game.Data;
using Game.Level;
using Game.Map.Controllers.Factory;
using Game.Map.Data;
using Game.Map.Models;
using Game.Map.Views.Buildings;
using Game.UI.Presenters;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Game.Map
{
    public class UpgradeableBuildingsPresenter : IStartable, IDisposable
    {
        private readonly MapBuildingsFactory _factory;
        private readonly IEnumerable<FactoryBuildingModel> _factoryModels;
        private readonly UIIconsSO _icons;
        private readonly IPopupPresenter<FactoryBuildingModel> _popupPresenter;
        private readonly ILevelsManager _levelsManager;

        private Dictionary<int, BuildingView> _views;
        private Dictionary<BuildingType, int> _buildingTypes;

        public UpgradeableBuildingsPresenter(MapBuildingsFactory factory,
            IEnumerable<FactoryBuildingModel> models, UIIconsSO icons,
            IPopupPresenter<FactoryBuildingModel> popupPresenter, ILevelsManager levelsManager)
        {
            _factory = factory;
            _factoryModels = models;
            _icons = icons;
            _popupPresenter = popupPresenter;
            _levelsManager = levelsManager;
        }

        public void Start()
        {
            _levelsManager.OnLevelComplete += OnLevelComplete;
            _views = new Dictionary<int, BuildingView>();
            _buildingTypes = new Dictionary<BuildingType, int>();

            foreach (var buildingModel in _factoryModels)
            {
                CreateUpgradableBuildingView(buildingModel);
                buildingModel.Start();
            }
        }

        private void OnLevelComplete()
        {
            foreach (var buildingModel in _factoryModels)
            {
                buildingModel.ResetUpgrades();
            }
        }

        private void CreateUpgradableBuildingView(IUpgradableBuildingModel model)
        {
            if (_buildingTypes.TryGetValue(model.Type, out var viewIndex))
            {
                _views[viewIndex].OnClicked -= OnFactoryClickedHandler;
                Object.Destroy(_views[viewIndex].gameObject);
                _buildingTypes.Remove(model.Type);
                _views.Remove(viewIndex);
            }

            var factoryBuilding = _factory.Create(model);
            var instanceId = factoryBuilding.GetInstanceID();
            factoryBuilding.HudMarker.SetIcon(GetHudIcon(model.Type));
            factoryBuilding.OnClicked += OnFactoryClickedHandler;
            _buildingTypes.TryAdd(model.Type, instanceId);
            _views.TryAdd(instanceId, factoryBuilding);
        }

        private Sprite GetHudIcon(BuildingType buildingType)
        {
            switch (buildingType)
            {
                case BuildingType.IronFactory:
                    return _icons.GetResourceItemIcon(ResourceItemType.Iron);
                case BuildingType.StoneFactory:
                    return _icons.GetResourceItemIcon(ResourceItemType.Stone);
                case BuildingType.WoodFactory:
                    return _icons.GetResourceItemIcon(ResourceItemType.Wood);
                case BuildingType.ToolsFactory:
                    return _icons.CancelIcon;
                default:
                    return null;
            }
        }

        private void OnFactoryClickedHandler(int viewIndex)
        {
            foreach (var buildingModel in _factoryModels)
            {
                if (_buildingTypes.TryGetValue(buildingModel.Type, out var index) && index == viewIndex)
                {
                    _popupPresenter.OpenPopup(buildingModel, () => { CreateUpgradableBuildingView(buildingModel); });
                    break;
                }
            }
        }

        public void Dispose()
        {
            _levelsManager.OnLevelComplete -= OnLevelComplete;
            
            foreach (var building in _views)
            {
                if (building.Value != null)
                {
                    building.Value.OnClicked -= OnFactoryClickedHandler;
                }
            }
        }
    }
}