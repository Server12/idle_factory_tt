using System.Collections.Generic;
using System.Linq;
using Game.Saves;
using Newtonsoft.Json;

namespace Game.Map.Data
{
    [JsonObject(MemberSerialization.Fields)]
    public class BuildingUpgradesSaveData : BaseSaveObject
    {
        private Dictionary<BuildingType, int> upgrades = new Dictionary<BuildingType, int>();

        public void Reset(BuildingType factoryType)
        {
            if (upgrades.ContainsKey(factoryType))
            {
                upgrades[factoryType] = 0;
            }
        }

        public void SetUpgrade(BuildingType factoryType, int upgradeIndex)
        {
            if (!upgrades.TryAdd(factoryType, upgradeIndex))
            {
                upgrades[factoryType] = upgradeIndex;
            }
        }

        public int GetUpgrade(BuildingType factoryType)
        {
            return upgrades.GetValueOrDefault(factoryType, 0);
        }

        public override string Name => "building_upgrades";
    }
}