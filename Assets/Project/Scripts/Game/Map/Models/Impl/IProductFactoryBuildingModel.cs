using System;
using Game.Data;
using Game.Map.Data;

namespace Game.Map.Models
{
    public interface IProductFactoryBuildingModel:IBuildingModel
    {
        public event Action<IProductFactoryBuildingModel> OnProductionStateChange;
        
        ResourceItemType ProductType { get; }
        
        void Start();

        void Stop();
        
        BuildingProductionState CurrentState { get; }
    }
}