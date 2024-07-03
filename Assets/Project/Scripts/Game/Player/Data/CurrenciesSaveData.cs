using System.Linq;
using Game.Data;

namespace Game.Player.Data
{
    public class CurrenciesSaveData : BasePlayerSaveDataGeneric<CurrencyType>
    {
        public CurrenciesSaveData(CurrencyItem[] startupItems = null)
        {
            if (startupItems != null)
            {
                items = startupItems.ToDictionary(item => item.ItemType, item => item.Count);
            }
        }

        public override string Name => "balance";
    }
}