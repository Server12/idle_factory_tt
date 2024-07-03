using System;
using Game.Map.Data;
using UnityEngine;

namespace Game.Level.Data
{
    [Serializable]
    public struct BuildingMapPosition
    {
        [SerializeField] private BuildingType _buildingType;
        [SerializeField] private Vector2Int _gridPos;

        public BuildingType BuildingType => _buildingType;

        public Vector2Int GridPos => _gridPos;
    }
}