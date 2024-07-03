using System.Collections.Generic;
using System.Linq;
using Game.Extensions;
using Game.Level.Data;
using Game.Tasks.Data;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Configs.Levels
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Create/LevelData", order = 0)]
    public class LevelDataSO : ScriptableObject
    {
        [SerializeField] private TaskListDataSO _taskList;

        [Scene] [SerializeField] private string _levelScene;

        [SerializeField] private BuildingMapPosition[] _mapPositions;

        [ReadOnly] [SerializeField] private List<int> _tasks;

        public IReadOnlyList<BaseTaskData> GetTasks()
        {
            var list = new List<BaseTaskData>();
            foreach (var taskId in _tasks)
            {
                var baseTask = _taskList.TasksList.Find(task => task.ID == taskId);
                if (baseTask != null)
                {
                    list.Add(baseTask);
                }
            }

            return list;
        }

        public string LevelScene => _levelScene;

        public BuildingMapPosition[] MapPositions => _mapPositions;

#if UNITY_EDITOR

        [Header("EDITOR ONLY")] [Dropdown("GetTaskIds")] [SerializeField]
        private int _taskId;

        [Button]
        private void AddTask()
        {
            _tasks ??= new List<int>();
            if (!_tasks.Contains(_taskId))
            {
                _tasks.Add(_taskId);
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }

        [Button]
        private void ClearTaskIds()
        {
            _tasks.Clear();
        }


        private DropdownList<int> GetTaskIds
        {
            get
            {
                if (_taskList == null) return null;
                var list = new DropdownList<int>();
                foreach (var taskData in _taskList.TasksList)
                {
                    list.Add(taskData.TaskDescription, taskData.ID);
                }

                return list;
            }
        }

#endif
    }
}