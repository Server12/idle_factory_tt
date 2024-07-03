using Game.Saves;

namespace Game.Level.Data
{
    public class LevelSaveData : BaseSaveObject
    { 
        public override string Name => "level_data";

        public int LevelId { get; set; } = 0;

        public int LevelsCompleteCount { get; set; } = 0;

    }
}