using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Data;
using Game.Map.Data;
using Game.Player.Models;

namespace Game.Map.Models
{
    public class ToolsFactoryBuildingModel : ICraftBuildingModel, IDisposable
    {
        public event Action OnRecipeChanged;
        public event Action<IProductFactoryBuildingModel> OnProductionStateChange;

        private readonly InventoryModel _inventoryModel;
        private readonly ToolsBuildingData _toolsBuildingData;
        public BuildingType Type => _toolsBuildingData.BuildingType;


        public ResourceItemType ProductType => _productType;

        private ResourceItemType _productType = ResourceItemType.None;

        private ToolsCraftData _currentRecipe;

        private CancellationTokenSource _tokenSource;

        public ToolsFactoryBuildingModel(InventoryModel inventoryModel, ToolsBuildingData toolsBuildingData)
        {
            _inventoryModel = inventoryModel;
            _toolsBuildingData = toolsBuildingData;
        }

        public ToolsCraftData[] Recipes => _toolsBuildingData.CraftRecipes;
        

        public void SelectCraftRecipe(ToolsCraftData data)
        {
            _currentRecipe = data;
            _productType = data.CraftedItem;
            OnRecipeChanged?.Invoke();
        }


        public void Start()
        {
            if (CurrentState == BuildingProductionState.Started) return;
            CurrentState = BuildingProductionState.Started;
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
            _tokenSource = new CancellationTokenSource();
            StartCraftingAsync(_tokenSource.Token).Forget();
            OnProductionStateChange?.Invoke(this);
        }

        public void Stop()
        {
            if (CurrentState == BuildingProductionState.Stopped) return;
            CurrentState = BuildingProductionState.Stopped;
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
            _tokenSource = null;
            OnProductionStateChange?.Invoke(this);
        }

        private async UniTaskVoid StartCraftingAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (!_inventoryModel.Has(_currentRecipe.Item1) || !_inventoryModel.Has(_currentRecipe.Item2))
                {
                    Stop();
                    continue;
                }

                await UniTask.Delay((int)(_toolsBuildingData.CraftDelaySeconds * 1000f), DelayType.DeltaTime, token);

                _inventoryModel.Remove(_currentRecipe.Item1);
                _inventoryModel.Remove(_currentRecipe.Item2);
                _inventoryModel.Add(_currentRecipe.CraftedItem, 1);
            }
        }

        public BuildingProductionState CurrentState { get; private set; }

        public ToolsCraftData CurrentRecipe => _currentRecipe;


        public void Dispose()
        {
            _tokenSource?.Dispose();
            _tokenSource = null;
        }
    }
}