using System;
using Game.Data;
using Game.Player.Data;
using Game.Saves;
using VContainer.Unity;

namespace Game.Player.Models
{
    public class CurrencyBalanceModel : IInitializable
    {
        public event Action OnBalanceChanged;

        private readonly ISavesManager _savesManager;
        private readonly CurrencyItem[] _startUpItems;
        private readonly CurrenciesSaveData _saveData;

        public CurrencyBalanceModel(ISavesManager savesManager, CurrencyItem[] startUpItems)
        {
            _savesManager = savesManager;
            _startUpItems = startUpItems;
            _saveData = new CurrenciesSaveData(startUpItems);
        }

        public void ResetAll()
        {
            if (_startUpItems.Length > 0)
            {
                foreach (var startUpItem in _startUpItems)
                {
                    _saveData.SetItem(startUpItem.ItemType, startUpItem.Count);
                }
            }
            else
            {
                _saveData.SetItem(CurrencyType.Gold, 0);
            }

            _savesManager.Save(_saveData);
        }

        public bool CanDebit(CurrencyType currencyType, int price)
        {
            var count = GetBalance(currencyType);
            return count >= price;
        }

        public int GetBalance(CurrencyType currencyType)
        {
            return _saveData.GetCount(currencyType);
        }

        public void Credit(CurrencyItem currencyItem)
        {
            Credit(currencyItem.ItemType, currencyItem.Count);
        }

        public void Credit(CurrencyType currencyType, int value)
        {
            var count = _saveData.GetCount(currencyType);
            _saveData.SetItem(currencyType, count + Math.Abs(value));
            _savesManager.Save(_saveData);
            OnBalanceChanged?.Invoke();
        }

        public bool Debit(CurrencyItem currencyItem)
        {
            return Debit(currencyItem.ItemType, currencyItem.Count);
        }

        public bool Debit(CurrencyType currencyType, int value)
        {
            var count = _saveData.GetCount(currencyType);
            var targetBalance = count - Math.Abs(value);
            if (targetBalance >= 0)
            {
                _saveData.SetItem(currencyType, targetBalance);
                _savesManager.Save(_saveData);
                OnBalanceChanged?.Invoke();
                return true;
            }

            return false;
        }


        public void Initialize()
        {
            _savesManager.Load(_saveData);
        }
    }
}