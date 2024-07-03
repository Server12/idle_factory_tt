using System;
using Game.Data;
using Game.Player.Data;
using Game.Saves;
using VContainer.Unity;

namespace Game.Player.Models
{
    public sealed class InventoryModel : IInitializable
    {
        public event Action<InventoryChangedEventData> OnChanged;

        private readonly ISavesManager _savesManager;
        private readonly InventorySaveData _inventorySaveData;

        public InventoryModel(ISavesManager savesManager, InventoryResourceItem[] startUpItems)
        {
            _savesManager = savesManager;
            _inventorySaveData = new InventorySaveData(startUpItems);
        }

        public void ResetAll()
        {
            var enums = Enum.GetValues(typeof(ResourceItemType));
            foreach (var enumvalue in enums)
            {
                _inventorySaveData.SetItem((ResourceItemType)enumvalue, 0);
            }

            _savesManager.Save(_inventorySaveData);
        }

        public int Get(ResourceItemType resourceItemType)
        {
            return _inventorySaveData.GetCount(resourceItemType);
        }

        public void Add(ResourceItemType resourceItemType, int value)
        {
            var current = Get(resourceItemType);
            current += Math.Abs(value);
            _inventorySaveData.SetItem(resourceItemType, current);
            _savesManager.Save(_inventorySaveData);
            OnChanged?.Invoke(new InventoryChangedEventData(value, resourceItemType, InventoryChangeOp.Added, current));
        }

        public bool Remove(ResourceItemType itemType, int count = 1)
        {
            var removeCount = Math.Abs(count);
            var itemCount = _inventorySaveData.GetCount(itemType);
            if (itemCount >= removeCount)
            {
                _inventorySaveData.SetItem(itemType, itemCount - removeCount);
                _savesManager.Save(_inventorySaveData);
                OnChanged?.Invoke(new InventoryChangedEventData(removeCount, itemType, InventoryChangeOp.Removed,
                    _inventorySaveData.GetCount(itemType)));
                return true;
            }

            return false;
        }

        public bool Has(ResourceItemType resourceItemType)
        {
            return _inventorySaveData.HasItem(resourceItemType);
        }

        public void Initialize()
        {
            _savesManager.Load(_inventorySaveData);
        }
    }
}