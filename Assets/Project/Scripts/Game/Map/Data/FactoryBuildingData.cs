using System;
using Game.Data;
using UnityEngine;

namespace Game.Map.Data
{
    [Serializable]
    public class FactoryBuildingData:BaseBuildingData
    {
        [SerializeField] private ResourceItemType _productType;
        [SerializeField] private FactoryBuildingUpgrade _upgradeData;
        
        public FactoryBuildingUpgrade UpgradeData => _upgradeData;

        public ResourceItemType ProductType => _productType;
    }
}