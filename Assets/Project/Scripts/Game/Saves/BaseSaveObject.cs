using Newtonsoft.Json;

namespace Game.Saves
{
    public abstract class BaseSaveObject
    {
        [JsonIgnore] public abstract string Name { get; }
        
        public virtual string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public virtual void Deserialize(string json)
        {
            JsonConvert.PopulateObject(json, this);
        }
    }
}