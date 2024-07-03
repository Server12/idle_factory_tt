namespace Game.Saves
{
    public interface ISavesManager
    {
        void Load(BaseSaveObject saveObject);

        void Save(BaseSaveObject saveObject);
    }
}