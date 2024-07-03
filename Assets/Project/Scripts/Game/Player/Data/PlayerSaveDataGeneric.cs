using System;
using System.Collections.Generic;
using Game.Saves;
using Newtonsoft.Json;

namespace Game.Player.Data
{
    [JsonObject(MemberSerialization.Fields)]
    public abstract class BasePlayerSaveDataGeneric<T>:BaseSaveObject where T : struct, Enum
    {
        protected Dictionary<T, int> items = new Dictionary<T, int>();


        public bool HasItem(T itemType)
        {
            return items.GetValueOrDefault(itemType, 0) > 0;
        }

        public void SetItem(T itemType, int value)
        {
            if (!items.TryAdd(itemType, value))
            {
                items[itemType] = value;
            }
        }

        public int GetCount(T itemType)
        {
            return items.GetValueOrDefault(itemType, 0);
        }
    }
}