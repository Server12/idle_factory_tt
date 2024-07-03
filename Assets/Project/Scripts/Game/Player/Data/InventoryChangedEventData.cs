using Game.Data;
using Game.Player.Data;

namespace Game.Player.Models
{
    public readonly struct InventoryChangedEventData
    {
        public readonly int Count;
        public readonly ResourceItemType ItemType;
        public readonly InventoryChangeOp ChangeOp;
        public readonly int Total;

        public InventoryChangedEventData(int count, ResourceItemType itemType, InventoryChangeOp changeOp, int total)
        {
            Count = count;
            ItemType = itemType;
            ChangeOp = changeOp;
            Total = total;
        }
    }
}