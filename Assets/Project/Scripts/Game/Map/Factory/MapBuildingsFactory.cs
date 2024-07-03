using System;
using Game.Configs.Map;
using Game.Extensions;
using Game.Level;
using Game.Map.Data;
using Game.Map.Models;
using Game.Map.Views.Buildings;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Map.Controllers.Factory
{
    public class MapBuildingsFactory
    {
        private readonly IMapController _mapController;
        private readonly ILevelsManager _levelsManager;
        private readonly BuildingViewsSO _buildingViewsSo;

        public MapBuildingsFactory(IMapController mapController, ILevelsManager levelsManager,
            BuildingViewsSO buildingViewsSo)
        {
            _mapController = mapController;
            _levelsManager = levelsManager;
            _buildingViewsSo = buildingViewsSo;
        }
        
        public BuildingView Create(ICraftBuildingModel model)
        {
            var levelData = _levelsManager.GetCurrentLevelData();
            var buildingPosition =
                levelData.MapPositions.Find(position => position.BuildingType == model.Type);
            var toolsBuilding = Object.Instantiate(_buildingViewsSo.ToolsFactoryBuildingViewPrefab,
                _mapController.BuildingsHolder);
            toolsBuilding.SetPosition(_mapController.Grid.GridToWorld(buildingPosition.GridPos));
            return toolsBuilding;
        }


        public BuildingView Create(IMarketBuildingModel model)
        {
            var levelData = _levelsManager.GetCurrentLevelData();
            var buildingPosition =
                levelData.MapPositions.Find(position => position.BuildingType == model.Type);
            var market = Object.Instantiate(_buildingViewsSo.MarketBuilding, _mapController.BuildingsHolder);
            market.SetPosition(_mapController.Grid.GridToWorld(buildingPosition.GridPos));
            return market;
        }


        public BuildingView Create(IUpgradableBuildingModel model)
        {
            var levelData = _levelsManager.GetCurrentLevelData();
            var views = _buildingViewsSo.FactoryBuildings.Find(o => o.Key == model.Type).Value;
            int upgradeLevel = Mathf.RoundToInt(((float)model.CurrentUpgradeLevel / model.MaxUpgrades) * (views.Length-1));
            var buildingPosition =
                levelData.MapPositions.Find(position => position.BuildingType == model.Type);
            var factoryBuilding = Object.Instantiate(views[upgradeLevel], _mapController.BuildingsHolder);
            factoryBuilding.SetPosition(_mapController.Grid.GridToWorld(buildingPosition.GridPos));
            return factoryBuilding;
        }
    }
}