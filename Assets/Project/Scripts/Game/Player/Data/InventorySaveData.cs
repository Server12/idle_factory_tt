using System.Linq;
using Game.Data;

namespace Game.Player.Data
{
    public class InventorySaveData : BasePlayerSaveDataGeneric<ResourceItemType>
    {
        public InventorySaveData(InventoryResourceItem[] startUpData = null)
        {
            if (startUpData != null)
            {
                items = startUpData.ToDictionary(item => item.ItemType, item => item.Count);
            }
        }

        public override string Name => "inventory";
    }
}