using System;
using System.Collections.Generic;
using Game.Map.Models;
using Game.Player.Models;
using Game.Tasks.Data;
using Game.Tasks.Models;
using VContainer;

namespace Game.Tasks.Factory
{
    public class TaskModelsFactory
    {
        private readonly IObjectResolver _resolver;

        public TaskModelsFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }


        public BaseTaskModel Create(BaseTaskData data)
        {
            if (data == null) throw new NullReferenceException();

            if (data is EarnCurrencyTaskData earnCurrencyTaskData)
            {
                return new EarnCurrencyTaskModel(earnCurrencyTaskData, _resolver.Resolve<CurrencyBalanceModel>());
            }

            if (data is ProduceResourceTaskData produceResourceTaskData)
            {
                return new ProduceResourceTaskModel(produceResourceTaskData, _resolver.Resolve<InventoryModel>());
            }

            if (data is UpgradeBuildingTaskData upgradeBuildingTaskData)
            {
                return new UpgradeBuildingTaskModel(upgradeBuildingTaskData,
                    _resolver.Resolve<IEnumerable<IUpgradableBuildingModel>>());
            }

            throw new NotImplementedException($"{data.GetType()}");
        }
    }
}