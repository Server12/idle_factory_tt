using System;
using Game.Data;
using Game.Player.Data;
using UnityEngine;

namespace Game.Map.Data
{
    public readonly struct UpgradeResult
    {
        public readonly int Index;
        public readonly int Price;
        public readonly int ProductionCount;
        public readonly float ProductionTime;

        public UpgradeResult(int index,int price, int productionCount, float productionTime)
        {
            Price = price;
            ProductionCount = productionCount;
            ProductionTime = productionTime;
            Index = index;
        }
    }
    
    [Serializable]
    public class FactoryBuildingUpgrade
    {
        [SerializeField] private CurrencyItem _basePrice;
        [SerializeField] private int _baseProductionCount = 1;
        [SerializeField] private float _baseProductionTimeSeconds = 5f;

        [Min(3)] [SerializeField] private int _maxUpgrades = 10;

        [SerializeField] private int _priceIncreaseKoof = 25;
        [Range(0f, 1f)] [SerializeField] private float _timeDecreaseMultiplier = 0.2f;
        [Range(0f, 1f)] [SerializeField] private float _productionCountIncreaseMultiplier = 0.2f;


        public int MaxUpgrades => _maxUpgrades;
        
        public int BaseProductionCount => _baseProductionCount;

        public float BaseProductionTimeSeconds => _baseProductionTimeSeconds;
        
        public float TimeDecreaseMultiplier => _timeDecreaseMultiplier;

        public float ProductionCountIncreaseMultiplier => _productionCountIncreaseMultiplier;

        public CurrencyItem BasePrice => _basePrice;

        public int PriceIncreaseKoof => _priceIncreaseKoof;
    }
}