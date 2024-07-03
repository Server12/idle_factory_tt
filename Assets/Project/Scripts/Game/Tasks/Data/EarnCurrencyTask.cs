using System;
using Game.Data;
using UnityEngine;

namespace Game.Tasks.Data
{
    [Serializable]
    public class EarnCurrencyTaskData : BaseTaskData
    {
        [SerializeField] private CurrencyType _currency;
        [SerializeField] private int _earnCount;

        public CurrencyType Currency => _currency;

        public int EarnCount => _earnCount;

#if UNITY_EDITOR
        public override void UpdateFromEditor()
        {
            _taskDescription = $"Earn {_earnCount} of {_currency}.";
            base.UpdateFromEditor();
        }
#endif
    }
}