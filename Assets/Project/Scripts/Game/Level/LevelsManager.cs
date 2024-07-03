using System;
using Cysharp.Threading.Tasks;
using Game.Configs.Levels;
using Game.Level.Data;
using Game.Saves;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Game.Level
{
    public class LevelsManager : ILevelsManager, IInitializable
    {
        private readonly ISavesManager _savesManager;
        private readonly LevelDataSO[] _levels;
        private readonly string _startScreenScene;

        private readonly LevelSaveData _saveData;

        private bool _isLastLevel;

        public event Action OnLevelComplete;

        public LevelsManager(ISavesManager savesManager, LevelDataSO[] levels, string startScreenScene)
        {
            _savesManager = savesManager;
            _levels = levels;
            _startScreenScene = startScreenScene;
            _saveData = new LevelSaveData();
        }


        public int CurrentLevel => _saveData.LevelId;

        public bool IsLastLevel => _isLastLevel;

        public void Initialize()
        {
            _savesManager.Load(_saveData);
        }

        public LevelDataSO GetCurrentLevelData()
        {
            return _levels[_saveData.LevelId];
        }

        public async UniTaskVoid LoadStartScreenAsync()
        {
            _isLastLevel = false;
            await SceneManager.LoadSceneAsync(_startScreenScene);
        }

        public async UniTaskVoid LoadLevelAsync()
        {
            var levelData = _levels[_saveData.LevelId];
            await SceneManager.LoadSceneAsync(levelData.LevelScene);
        }

        public void SetNextLevel()
        {
            OnLevelComplete?.Invoke();

            _saveData.LevelId++;
            if (_saveData.LevelId >= (_levels.Length - 1))
            {
                _isLastLevel = true;
                _saveData.LevelsCompleteCount++;
            }

            _saveData.LevelId = (int)Mathf.Repeat(_saveData.LevelId, _levels.Length);
            _savesManager.Save(_saveData);
        }
    }
}