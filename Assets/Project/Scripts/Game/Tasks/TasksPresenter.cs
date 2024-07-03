using System;
using System.Collections.Generic;
using System.Linq;
using Game.Extensions;
using Game.Level;
using Game.Tasks.Factory;
using Game.Tasks.Models;
using Game.UI.Components;
using Game.UI.Managers;
using Game.UI.Popups;
using TMPro;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Game.Tasks
{
    public class TasksPresenter : ITasksPresenter, IStartable, IDisposable
    {
        public event Action OnAllTasksComplete;
        public event Action<int> OnTaskCompleted;

        private readonly ILevelsManager _levelsManager;
        private readonly TaskModelsFactory _factory;
        private readonly IUIFactory _uiFactory;

        private readonly List<BaseTaskModel> _tasks;

        public bool IsAllTasksCompleted { get; private set; }

        public IReadOnlyList<ITaskModel> Tasks { get; private set; }

        private readonly HashSet<int> _completedTasks = new HashSet<int>();

        private readonly Dictionary<int, TaskItemPanel> _taskItemPanels;


        private TasksPopupView _tasksPopupView;

        public TasksPresenter(ILevelsManager levelsManager, TaskModelsFactory factory, IUIFactory uiFactory)
        {
            _levelsManager = levelsManager;
            _factory = factory;
            _uiFactory = uiFactory;
            _taskItemPanels = new Dictionary<int, TaskItemPanel>();
            _tasks = new List<BaseTaskModel>();
        }


        public void Start()
        {
            var levelTasks = _levelsManager.GetCurrentLevelData().GetTasks();

            foreach (var taskData in levelTasks)
            {
                var model = _factory.Create(taskData);

                model.Initialize();

                if (model.IsComplete)
                {
                    _completedTasks.Add(model.Id);
                }
                else
                {
                    model.OnComplete += OnTasksComplete;
                }


                _tasks.Add(model);
            }

            Tasks = _tasks.Cast<ITaskModel>().ToList();

            if (_completedTasks.Count == _tasks.Count)
            {
                IsAllTasksCompleted = true;
                OnAllTasksComplete?.Invoke();
            }
        }

        private void OnTasksComplete(int id)
        {
            var taskModel = _tasks.Find(model => model.Id == id);
            taskModel.OnComplete -= OnTasksComplete;

            if (_taskItemPanels.TryGetValue(id, out var panel))
            {
                panel.descriptionText.fontStyle = FontStyles.Strikethrough;
            }

            _completedTasks.Add(id);

            OnTaskCompleted?.Invoke(id);

            if (_completedTasks.Count == _tasks.Count)
            {
                IsAllTasksCompleted = true;
                OnAllTasksComplete?.Invoke();
            }
        }

        public void OpenTasksPopup()
        {
            if (_tasksPopupView == null)
            {
                _tasksPopupView = _uiFactory.GetOrCreatePopup<TasksPopupView>();

                _tasksPopupView.taskPanelInstance.gameObject.SetActive(false);

                foreach (var taskModel in _tasks)
                {
                    var panel = Object.Instantiate(_tasksPopupView.taskPanelInstance, _tasksPopupView.panelContent);
                    panel.gameObject.SetActive(true);
                    panel.descriptionText.text = taskModel.Description;
                    panel.descriptionText.fontStyle =
                        taskModel.IsComplete ? FontStyles.Strikethrough : FontStyles.Normal;
                    _taskItemPanels.Add(taskModel.Id, panel);
                }
            }

            _tasksPopupView.Open();
        }


        public void Dispose()
        {
            foreach (var taskModel in _tasks)
            {
                taskModel.OnComplete -= OnTasksComplete;
                taskModel.Dispose();
            }
        }
    }
}