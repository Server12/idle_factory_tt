using System;
using Game.Data;
using Game.Map.Data;
using UnityEngine;

namespace Game.Tasks.Data
{
    [Serializable]
    public class UpgradeBuildingTaskData : BaseTaskData
    {
        [SerializeField] private BuildingType _buildingType;
        [SerializeField] private int _upgradeLevel;

        public int UpgradeLevel => _upgradeLevel;

        public BuildingType BuildingType => _buildingType;

#if UNITY_EDITOR
        public override void UpdateFromEditor()
        {
            // _id = $"upgrade_{_buildingType.ToString().ToLower()}_{_upgradeLevel}";
            _taskDescription = $"Upgrade {_buildingType} to {_upgradeLevel} level.";
            base.UpdateFromEditor();
        }
#endif
    }
}