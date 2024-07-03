using System;
using Game.Data;
using UnityEngine;

namespace Game.Tasks.Data
{
    [Serializable]
    public class ProduceResourceTaskData : BaseTaskData
    {
        [SerializeField] private ResourceItemType _resource;
        [SerializeField] private int _count;

        public ResourceItemType Resource => _resource;

        public int Count => _count;

#if UNITY_EDITOR
        public override void UpdateFromEditor()
        {
           // _id = $"produce_{_resource.ToString().ToLower()}_{_count}";
            _taskDescription = $"Produce {_count} of {_resource}.";
            base.UpdateFromEditor();
        }
#endif
    }
}