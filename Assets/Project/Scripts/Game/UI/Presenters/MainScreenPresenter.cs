using System;
using Game.Data;
using Game.Level;
using Game.Player.Models;
using Game.Tasks;
using Game.UI.Managers;
using Game.UI.Popups;
using Game.UI.Screens;
using VContainer.Unity;

namespace Game.UI.Presenters
{
    public class MainScreenPresenter : IStartable, IDisposable
    {
        private readonly IUIFactory _uiFactory;
        private readonly InventoryModel _inventoryModel;
        private readonly CurrencyBalanceModel _balanceModel;
        private readonly ITasksPresenter _tasksPresenter;
        private readonly ILevelsManager _levelsManager;

        private GameOverPopupView _gameOverPopup;
        private MainScreenUIView _screenUIView;

        public MainScreenPresenter(IUIFactory uiFactory,
            InventoryModel inventoryModel,
            CurrencyBalanceModel balanceModel,
            ITasksPresenter tasksPresenter,
            ILevelsManager levelsManager)
        {
            _uiFactory = uiFactory;
            _inventoryModel = inventoryModel;
            _balanceModel = balanceModel;
            _tasksPresenter = tasksPresenter;
            _levelsManager = levelsManager;
        }

        public void Start()
        {
            _inventoryModel.OnChanged += OnInventoryChangedHandler;
            _balanceModel.OnBalanceChanged += OnBalanceChangedHandler;

            _screenUIView = _uiFactory.GetOrCreateScreen<MainScreenUIView>();

            if (!_tasksPresenter.IsAllTasksCompleted)
            {
                _tasksPresenter.OnTaskCompleted += OnTaskCompleteHandler;
                _tasksPresenter.OnAllTasksComplete += OnAllTasksCompleteHandler;
                _screenUIView.tasksButton.OnClick += OnTasksButtonClickHandler;
            }
            else
            {
                OnAllTasksCompleteHandler();
            }

            _screenUIView.CreateOrUpdateCurrencyPanel(CurrencyType.Gold,
                _balanceModel.GetBalance(CurrencyType.Gold));

            _screenUIView.CreateOrUpdateResourcePanel(ResourceItemType.Iron,
                _inventoryModel.Get(ResourceItemType.Iron));

            _screenUIView.CreateOrUpdateResourcePanel(ResourceItemType.Stone,
                _inventoryModel.Get(ResourceItemType.Stone));

            _screenUIView.CreateOrUpdateResourcePanel(ResourceItemType.Wood,
                _inventoryModel.Get(ResourceItemType.Wood));

            _screenUIView.CreateOrUpdateResourcePanel(ResourceItemType.Pitchfork,
                _inventoryModel.Get(ResourceItemType.Pitchfork));

            _screenUIView.CreateOrUpdateResourcePanel(ResourceItemType.Hummer,
                _inventoryModel.Get(ResourceItemType.Hummer));

            _screenUIView.CreateOrUpdateResourcePanel(ResourceItemType.Drill,
                _inventoryModel.Get(ResourceItemType.Drill));
        }

        private void OnTaskCompleteHandler(int id)
        {
            _screenUIView.tasksButton.PingTaskComplete();
        }

        private void OnNextLevelClickHandler()
        {
            _balanceModel.ResetAll();
            _inventoryModel.ResetAll();

            _screenUIView.nextLevelButton.OnClick -= OnNextLevelClickHandler;
            _levelsManager.SetNextLevel();
            _levelsManager.LoadLevelAsync().Forget();
        }

        private void OnAllTasksCompleteHandler()
        {
            _screenUIView.tasksButton.OnClick -= OnTasksButtonClickHandler;
            _screenUIView.tasksButton.gameObject.SetActive(false);
            if (!_levelsManager.IsLastLevel)
            {
                _screenUIView.nextLevelButton.gameObject.SetActive(true);
                _screenUIView.nextLevelButton.OnClick += OnNextLevelClickHandler;
            }
            else
            {
                _gameOverPopup = _uiFactory.GetOrCreatePopup<GameOverPopupView>();
                _gameOverPopup.restartButton.OnClick += OnRestartGameHandler;
                _gameOverPopup.Open();
            }
        }

        private void OnRestartGameHandler()
        {
            _balanceModel.ResetAll();
            _inventoryModel.ResetAll();
            _levelsManager.SetNextLevel();
            _levelsManager.LoadStartScreenAsync().Forget();
        }

        private void OnTasksButtonClickHandler()
        {
            _screenUIView.tasksButton.StopPingCompletedTask();
            _tasksPresenter.OpenTasksPopup();
        }

        private void OnBalanceChangedHandler()
        {
            _screenUIView.CreateOrUpdateCurrencyPanel(CurrencyType.Gold,
                _balanceModel.GetBalance(CurrencyType.Gold));
        }

        private void OnInventoryChangedHandler(InventoryChangedEventData data)
        {
            if (data.ItemType == ResourceItemType.None) return;
            _screenUIView.CreateOrUpdateResourcePanel(data.ItemType, data.Total);
        }

        public void Dispose()
        {
            if (_gameOverPopup != null)
            {
                _gameOverPopup.restartButton.OnClick -= OnRestartGameHandler;
            }

            _tasksPresenter.OnTaskCompleted -= OnTaskCompleteHandler;
            _tasksPresenter.OnAllTasksComplete -= OnAllTasksCompleteHandler;
            _screenUIView.tasksButton.OnClick -= OnTasksButtonClickHandler;
            _inventoryModel.OnChanged -= OnInventoryChangedHandler;
            _balanceModel.OnBalanceChanged -= OnBalanceChangedHandler;
        }
    }
}