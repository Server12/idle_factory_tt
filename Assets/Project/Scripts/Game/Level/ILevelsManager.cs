using System;
using Cysharp.Threading.Tasks;
using Game.Configs.Levels;
using Game.Level.Data;

namespace Game.Level
{
    public interface ILevelsManager
    {
        event Action OnLevelComplete;

        int CurrentLevel { get; }

        bool IsLastLevel { get; }

        LevelDataSO GetCurrentLevelData();

        void SetNextLevel();

        UniTaskVoid LoadLevelAsync();

        UniTaskVoid LoadStartScreenAsync();
    }
}