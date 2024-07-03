using Game.Configs.Levels;
using Game.Extensions;
using Game.Level.Data;
using Game.Map.Data;
using Game.Player.Data;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "GameData", menuName = "Create/GameData", order = 0)]
    public sealed class GameDataSO : ScriptableObject
    {
        [Header("BUILDINGS")]
        [SerializeReference,SubclassSelector] private BaseBuildingData[] _buildings;

        [Header("STARTUP RESOURCES")]
        [SerializeField] private PlayerData startUpPlayer;


        [Header("LEVELS")]
        [Scene]
        [SerializeField] private string _startScreenScene;
        [SerializeField] private LevelDataSO[] _levels;
        
        public BaseBuildingData[] Buildings => _buildings;

        public PlayerData StartUpPlayer => startUpPlayer;

        public LevelDataSO[] Levels => _levels;

        public string StartScreenScene => _startScreenScene;
    }
}