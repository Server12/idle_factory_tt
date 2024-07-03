using Game.Data;
using Game.Map.Data;

namespace Game.Map.Models
{
    public interface IMarketBuildingModel:IBuildingModel
    {
        MarketSaleItem[] SaleItems { get; }

        int GetSalesCount(ResourceItemType resourceItemType);
        
        bool CanSale(MarketSaleItem saleItem);

        bool Sale(MarketSaleItem saleItem);
    }
}