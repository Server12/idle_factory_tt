using System;
using Game.Data;
using UnityEngine;

namespace Game.Player.Data
{
    [Serializable]
    public struct InventoryResourceItem
    {
        [SerializeField] private ResourceItemType _resourceItemType;
        [SerializeField] private int _count;


        public InventoryResourceItem(ResourceItemType itemType, int count)
        {
            _resourceItemType = itemType;
            _count = count;
        }

        public ResourceItemType ItemType => _resourceItemType;

        public int Count
        {
            get => _count;
        }
    }

    [Serializable]
    public struct CurrencyItem
    {
        [SerializeField] private CurrencyType _currencyType;
        [SerializeField] private int _count;


        public CurrencyItem(CurrencyType itemType, int count)
        {
            _currencyType = itemType;
            _count = count;
        }

        public CurrencyType ItemType => _currencyType;

        public int Count
        {
            get => _count;
        }
    }
}