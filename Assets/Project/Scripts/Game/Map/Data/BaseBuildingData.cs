using System;
using UnityEngine;

namespace Game.Map.Data
{
    [Serializable]
    public abstract class BaseBuildingData
    {
        [SerializeField] private BuildingType _buildingType;

        public BuildingType BuildingType => _buildingType;
    }
}