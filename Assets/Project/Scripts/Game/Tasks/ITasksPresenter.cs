using System;
using System.Collections.Generic;
using Game.Tasks.Models;

namespace Game.Tasks
{
    public interface ITasksPresenter
    {
        event Action OnAllTasksComplete;

        event Action<int> OnTaskCompleted;

        bool IsAllTasksCompleted { get; }

        void OpenTasksPopup();
        
        IReadOnlyList<ITaskModel> Tasks { get; }
    }
}