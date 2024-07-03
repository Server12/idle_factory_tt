using Game.Data;
using Game.Map.Data;
using Game.Map.Views.Buildings;
using UnityEngine;

namespace Game.Configs.Map
{
    [CreateAssetMenu(fileName = "BuildingViewsSO", menuName = "Create/BuildingViewsSO", order = 0)]
    public class BuildingViewsSO : ScriptableObject
    {
      
        [SerializeField] private UnityEnumObject<BuildingType,BuildingView[]>[] _factoryBuildings;

        [SerializeField] private BuildingView _toolsFactoryBuildingViewPrefab;
        
        [SerializeField] private BuildingView marketPrefab;
        
        public BuildingView MarketBuilding => marketPrefab;

        public UnityEnumObject<BuildingType, BuildingView[]>[] FactoryBuildings => _factoryBuildings;

        public BuildingView ToolsFactoryBuildingViewPrefab => _toolsFactoryBuildingViewPrefab;
    }
}