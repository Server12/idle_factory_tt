using System.Collections.Generic;
using Game.Map.Data;
using Game.Map.Models;
using Game.Tasks.Data;

namespace Game.Tasks.Models
{
    public class UpgradeBuildingTaskModel : BaseTaskModel
    {
        private readonly UpgradeBuildingTaskData _upgradeBuildingTaskData;
        private readonly IEnumerable<IUpgradableBuildingModel> _buildingModels;

        private IUpgradableBuildingModel _checkBuilding;

        public UpgradeBuildingTaskModel(UpgradeBuildingTaskData upgradeBuildingTaskData,
            IEnumerable<IUpgradableBuildingModel> buildingModels) : base(upgradeBuildingTaskData)
        {
            _upgradeBuildingTaskData = upgradeBuildingTaskData;
            _buildingModels = buildingModels;
        }


        public override void Initialize()
        {
            foreach (var buildingModel in _buildingModels)
            {
                if (buildingModel.Type == _upgradeBuildingTaskData.BuildingType)
                {
                    _checkBuilding = buildingModel;
                    break;
                }
            }

            _checkBuilding.OnUpgraded += OnBuildingUpgradedHandler;
            OnBuildingUpgradedHandler(_upgradeBuildingTaskData.BuildingType);
        }

        private void OnBuildingUpgradedHandler(BuildingType type)
        {
            var level = _checkBuilding.CurrentUpgradeLevel + 1;

            if (level >= _upgradeBuildingTaskData.UpgradeLevel)
            {
                _checkBuilding.OnUpgraded -= OnBuildingUpgradedHandler;
                IsComplete = true;
                RaiseTaskComplete();
            }
        }

        public override void Dispose()
        {
            if (_checkBuilding != null)
            {
                _checkBuilding.OnUpgraded -= OnBuildingUpgradedHandler;
            }

            base.Dispose();
        }
    }
}