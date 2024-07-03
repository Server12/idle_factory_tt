using System;
using Game.Data;
using Game.Player.Data;
using UnityEngine;

namespace Game.Map.Data
{
    [Serializable]
    public struct MarketSaleItem
    {
        [SerializeField] private CurrencyItem _price;
        [SerializeField] private ResourceItemType resourceItemType;


        public CurrencyItem Price => _price;

        public ResourceItemType ResourceItemType => resourceItemType;
    }
    
    [Serializable]
    public class MarketBuildingData:BaseBuildingData
    {
        [SerializeField] private MarketSaleItem[] _saleItems;

        public MarketSaleItem[] SaleItems => _saleItems;
    }
}