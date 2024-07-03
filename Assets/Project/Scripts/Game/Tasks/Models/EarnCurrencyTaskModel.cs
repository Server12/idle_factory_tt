using System;
using Game.Player.Models;
using Game.Tasks.Data;

namespace Game.Tasks.Models
{
    public class EarnCurrencyTaskModel : BaseTaskModel
    {
        private readonly EarnCurrencyTaskData _earnCurrencyTaskData;
        private readonly CurrencyBalanceModel _balanceModel;

        public EarnCurrencyTaskModel(EarnCurrencyTaskData earnCurrencyTaskData, CurrencyBalanceModel balanceModel) :
            base(earnCurrencyTaskData)
        {
            _earnCurrencyTaskData = earnCurrencyTaskData;
            _balanceModel = balanceModel;
        }

        public override void Initialize()
        {
            _balanceModel.OnBalanceChanged += OnBalanceChangedHandler;
            OnBalanceChangedHandler();
        }

        private void OnBalanceChangedHandler()
        {
            IsComplete = _balanceModel.GetBalance(_earnCurrencyTaskData.Currency) >= _earnCurrencyTaskData.EarnCount;
            if (IsComplete)
            {
                RaiseTaskComplete();
            }
        }

        public override void Dispose()
        {
            _balanceModel.OnBalanceChanged -= OnBalanceChangedHandler;
            base.Dispose();
        }
    }
}