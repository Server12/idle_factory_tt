using System;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Tasks.Data
{
    [Serializable]
    public abstract class BaseTaskData
    {
        [ReadOnly] [SerializeField] private int _id;
        [SerializeField] protected string _taskDescription;

        public string TaskDescription => _taskDescription;

        public int ID => _id;

#if UNITY_EDITOR

        public virtual void UpdateFromEditor()
        {
            if (_id == 0)
            {
                _id = Guid.NewGuid().GetHashCode();
            }
        }

#endif
    }
}