using Game.Player.Models;
using Game.Tasks.Data;

namespace Game.Tasks.Models
{
    public class ProduceResourceTaskModel : BaseTaskModel
    {
        private readonly ProduceResourceTaskData _taskData;
        private readonly InventoryModel _inventoryModel;

        public ProduceResourceTaskModel(ProduceResourceTaskData taskData, InventoryModel inventoryModel) :
            base(taskData)
        {
            _taskData = taskData;
            _inventoryModel = inventoryModel;
        }

        public override void Initialize()
        {
            IsComplete = _inventoryModel.Get(_taskData.Resource) >= _taskData.Count;
            if (!IsComplete)
            {
                _inventoryModel.OnChanged += OinInventoryChangedHandler;
            }
        }

        private void OinInventoryChangedHandler(InventoryChangedEventData data)
        {
            if (_taskData.Resource == data.ItemType)
            {
                if (data.Total >= _taskData.Count)
                {
                    _inventoryModel.OnChanged -= OinInventoryChangedHandler;
                    IsComplete = true;
                    RaiseTaskComplete();
                }
            }
        }

        public override void Dispose()
        {
            _inventoryModel.OnChanged -= OinInventoryChangedHandler;
            base.Dispose();
        }
    }
}