using System;
using Game.Level;
using Game.UI.Managers;
using Game.UI.Screens;
using VContainer.Unity;

namespace Game.UI.Presenters
{
    public class StartScreenPresenter : IStartable, IDisposable
    {
        private readonly IUIFactory _factory;
        private readonly ILevelsManager _levelsManager;

        private StartScreenView _startScreen;

        public StartScreenPresenter(IUIFactory factory, ILevelsManager levelsManager)
        {
            _factory = factory;
            _levelsManager = levelsManager;
        }

        public void Start()
        {
            _startScreen = _factory.GetOrCreateScreen<StartScreenView>();
            _startScreen.startGameButton.OnClick += OnStartGameClickHandler;
        }

        private void OnStartGameClickHandler()
        {
            _startScreen.startGameButton.Enabled = false;
            _levelsManager.LoadLevelAsync().Forget();
        }

        public void Dispose()
        {
            if (_startScreen != null)
            {
                _startScreen.startGameButton.OnClick -= OnStartGameClickHandler;
            }
        }
    }
}