using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Saves
{
    public class SavesManager : ISavesManager
    {
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.None,
            Formatting = Formatting.None
        };
        

        public void Load(BaseSaveObject saveObject)
        {
            if (PlayerPrefs.HasKey(saveObject.Name))
            {
                saveObject.Deserialize(PlayerPrefs.GetString(saveObject.Name));
            }
        }

        public void Save(BaseSaveObject saveObject)
        {
            PlayerPrefs.SetString(saveObject.Name, saveObject.Serialize());
            PlayerPrefs.Save();
        }
    }
}