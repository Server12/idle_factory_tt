using System;
using System.Collections.Generic;
using Game.Tasks.Data;
using NaughtyAttributes;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "TaskListData", menuName = "Create/TaskListData", order = 0)]
    public class TaskListDataSO : ScriptableObject
#if UNITY_EDITOR
        , ISerializationCallbackReceiver
#endif
    {
        [SerializeReference, SubclassSelector] private BaseTaskData[] _tasksList;

        public BaseTaskData[] TasksList => _tasksList;

#if UNITY_EDITOR

        [SerializeField] private TextAsset _backupTasksJson;

        [Button]
        private void LoadFromBackUp()
        {
            var path = UnityEditor.EditorUtility.OpenFilePanel("Open taskslist backup", Application.dataPath, "asset");
            if (!string.IsNullOrEmpty(path))
            {
                var asset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                if (asset != null)
                {
                    _tasksList = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseTaskData[]>(asset.text);
                }
            }
        }

        [Button]
        private void SerializeToJson()
        {
            var path = UnityEditor.EditorUtility.SaveFilePanelInProject("Backup TasksList", "tasks_list", "asset",
                "Select path for TextAsset");
            if (!string.IsNullOrEmpty(path))
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(_tasksList, Formatting.Indented,
                    new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

                UnityEditor.AssetDatabase.CreateAsset(new TextAsset(json), path);
                UnityEditor.AssetDatabase.SaveAssets();
            }
        }

        [Button]
        private void Validate()
        {
            var ids = new HashSet<int>();

            foreach (var taskData in _tasksList)
            {
                if (!ids.Add(taskData.ID))
                {
                    throw new Exception($"Validation error:{taskData.ID} = 0 or duplicates");
                }
            }
            Debug.Log("TaskList Validation Complete");
        }


        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            var id = 0;
            foreach (var taskData in _tasksList)
            {
                taskData?.UpdateFromEditor();
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
        }
#endif
    }
}