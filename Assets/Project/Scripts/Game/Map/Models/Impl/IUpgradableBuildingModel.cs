using System;
using Game.Data;
using Game.Map.Data;

namespace Game.Map.Models
{
    public interface IUpgradableBuildingModel : IProductFactoryBuildingModel
    {
        event Action<BuildingType> OnUpgraded;
        
        int MaxUpgrades { get; }
        
        int CurrentUpgradeLevel { get; }

        int CurrentPrice { get; }

        float ProductionTime { get; }

        int ProductionCount { get; }

        UpgradeResult? NextUpgrade { get; }
        
        CurrencyType CurrencyType { get; }
        
    }
}