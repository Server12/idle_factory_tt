using System;
using UnityEngine;

namespace Game.Player.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private CurrencyItem[] _currencyItems;

        [SerializeField] private InventoryResourceItem[] _resourceItems;

        public InventoryResourceItem[] ResourceItems => _resourceItems;

        public CurrencyItem[] CurrencyItems => _currencyItems;
    }
}