using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Data;
using Game.Level;
using Game.Map.Data;
using Game.Player.Models;
using Game.Saves;
using UnityEngine;
using VContainer.Unity;

namespace Game.Map.Models
{
    public class FactoryBuildingModel : IUpgradableBuildingModel, IInitializable, IDisposable
    {
        private readonly ISavesManager _savesManager;
        private readonly InventoryModel _inventoryModel;
        public event Action<BuildingType> OnUpgraded;
        public event Action<IProductFactoryBuildingModel> OnProductionStateChange;

        public CurrencyType CurrencyType { get; private set; }


        public int MaxUpgrades => _upgradeData.MaxUpgrades;
        public int CurrentUpgradeLevel { get; private set; }

        public int CurrentPrice { get; private set; }

        public float ProductionTime { get; private set; }

        public int ProductionCount { get; private set; }

        public BuildingType Type { get; }

        public ResourceItemType ProductType { get; }

        public BuildingProductionState CurrentState { get; private set; }

        private readonly BuildingUpgradesSaveData _buildingUpgradesSave = new BuildingUpgradesSaveData();

        private readonly FactoryBuildingUpgrade _upgradeData;

        private CancellationTokenSource _tokenSource;

        private float _currentProductionTime;
        private float _produceTimer = 0;

        public FactoryBuildingModel(ISavesManager savesManager, FactoryBuildingData factoryBuildingData,
            InventoryModel inventoryModel)
        {
            _savesManager = savesManager;
            _inventoryModel = inventoryModel;
            ProductType = factoryBuildingData.ProductType;
            Type = factoryBuildingData.BuildingType;
            _upgradeData = factoryBuildingData.UpgradeData;
            CurrencyType = _upgradeData.BasePrice.ItemType;
        }

        public UpgradeResult? NextUpgrade => CalcUpgrade(CurrentUpgradeLevel + 1);

        public void ResetUpgrades()
        {
            Stop();
            _buildingUpgradesSave.Reset(Type);
            _savesManager.Save(_buildingUpgradesSave);
            CurrentUpgradeLevel = 0;
        }

        public bool Upgrade(int upgradeIndex)
        {
            var result = CalcUpgrade(upgradeIndex);

            if (result != null)
            {
                ProductionCount = result.Value.ProductionCount;
                ProductionTime = result.Value.ProductionTime;
                CurrentPrice = result.Value.Price;
                CurrentUpgradeLevel = result.Value.Index;
                _buildingUpgradesSave.SetUpgrade(Type, CurrentUpgradeLevel);
                _savesManager.Save(_buildingUpgradesSave);

                OnUpgraded?.Invoke(Type);

                return true;
            }

            return false;
        }


        public void Start()
        {
            if (CurrentState == BuildingProductionState.Started) return;
            CurrentState = BuildingProductionState.Started;
            _tokenSource = new CancellationTokenSource();
            StartProduceAsync(_tokenSource.Token).Forget();
            OnProductionStateChange?.Invoke(this);
        }

        public void Stop()
        {
            if (CurrentState == BuildingProductionState.Stopped) return;
            CurrentState = BuildingProductionState.Stopped;
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
            OnProductionStateChange?.Invoke(this);
        }

        private async UniTaskVoid StartProduceAsync(CancellationToken token)
        {
            _currentProductionTime = ProductionTime;
            _produceTimer = 0;
            while (!token.IsCancellationRequested)
            {
                await UniTask.Yield(PlayerLoopTiming.Update, token);
                _produceTimer += Time.deltaTime;
                if (_produceTimer >= _currentProductionTime)
                {
                    _produceTimer = 0;
                    _currentProductionTime = ProductionTime;
                    _inventoryModel.Add(ProductType, ProductionCount);
                }
            }
        }


        private UpgradeResult? CalcUpgrade(int upgradeIndex)
        {
            var currentIndex = Math.Clamp(upgradeIndex, 0, _upgradeData.MaxUpgrades);

            if (currentIndex == MaxUpgrades) return null;

            var price = _upgradeData.BasePrice.Count + (currentIndex * _upgradeData.PriceIncreaseKoof);

            var productionCount = _upgradeData.BaseProductionCount +
                                  Mathf.FloorToInt(currentIndex * _upgradeData.ProductionCountIncreaseMultiplier);

            var productionTime = Mathf.Max(1f,
                _upgradeData.BaseProductionTimeSeconds - currentIndex * _upgradeData.TimeDecreaseMultiplier);

            return new UpgradeResult(currentIndex, price, productionCount, productionTime);
        }

        public void Initialize()
        {
            _savesManager.Load(_buildingUpgradesSave);
            Upgrade(_buildingUpgradesSave.GetUpgrade(Type));
        }

        public void Dispose()
        {
            _tokenSource?.Dispose();
            _tokenSource = null;
        }
    }
}