using System;
using Game.Data;
using Game.Map.Data;
using Game.Player.Models;

namespace Game.Map.Models
{
    [Serializable]
    public class MarketBuildingModel : IMarketBuildingModel
    {
        private readonly InventoryModel _inventoryModel;
        private readonly CurrencyBalanceModel _balanceModel;
        private readonly MarketBuildingData _marketBuildingData;

        public BuildingType Type => _marketBuildingData.BuildingType;

        public MarketBuildingModel(InventoryModel inventoryModel, CurrencyBalanceModel balanceModel,
            MarketBuildingData marketBuildingData)
        {
            _inventoryModel = inventoryModel;
            _balanceModel = balanceModel;
            _marketBuildingData = marketBuildingData;
        }

        public MarketSaleItem[] SaleItems => _marketBuildingData.SaleItems;

        public int GetSalesCount(ResourceItemType resourceItemType)
        {
            return _inventoryModel.Get(resourceItemType);
        }

        public bool CanSale(MarketSaleItem saleItem)
        {
            return _inventoryModel.Has(saleItem.ResourceItemType);
        }

        public bool Sale(MarketSaleItem saleItem)
        {
            if (_inventoryModel.Has(saleItem.ResourceItemType))
            {
                var count = _inventoryModel.Get(saleItem.ResourceItemType);
                _inventoryModel.Remove(saleItem.ResourceItemType, count);
                _balanceModel.Credit(saleItem.Price.ItemType, saleItem.Price.Count * count);
                return true;
            }

            return false;
        }
    }
}