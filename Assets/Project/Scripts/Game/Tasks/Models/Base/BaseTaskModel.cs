using System;
using Game.Tasks.Data;

namespace Game.Tasks.Models
{
    public abstract class BaseTaskModel : ITaskModel,IDisposable
    {
        public event Action<int> OnComplete;
        
        public int Id { get; }

        public string Description { get; }
        
        public bool IsComplete { get; protected set; }

        protected BaseTaskModel(BaseTaskData taskData)
        {
            Id = taskData.ID;
            Description = taskData.TaskDescription;
        }

        public abstract void Initialize();
        
        protected void RaiseTaskComplete()
        {
            OnComplete?.Invoke(Id);
        }

        public virtual void Dispose()
        {
            
        }
    }
}