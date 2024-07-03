using System;
using Game.Map.Models;
using Game.Player.Models;
using Game.UI.Managers;
using Game.UI.Popups;
using VContainer.Unity;

namespace Game.UI.Presenters
{
    public class ResourceFactoryPopupsPresenter : IPopupPresenter<FactoryBuildingModel>, IInitializable, IDisposable
    {
        private readonly IUIFactory _factory;
        private readonly CurrencyBalanceModel _balanceModel;

        private ResourceItemFactoryPopup _factoryPopup;

        private FactoryBuildingModel _model;
        private Action _upgradeCompleteCallback;


        public ResourceFactoryPopupsPresenter(IUIFactory factory, CurrencyBalanceModel balanceModel)
        {
            _factory = factory;
            _balanceModel = balanceModel;
        }

        public void Initialize()
        {
            _factoryPopup = _factory.GetOrCreatePopup<ResourceItemFactoryPopup>();
            _factoryPopup.UpgradeButton.OnClick += OnUpgradeClickHandler;
        }

        private void OnUpgradeClickHandler()
        {
            if (_model != null)
            {
                if (_balanceModel.Debit(_model.CurrencyType, _model.CurrentPrice))
                {
                    if (_model.Upgrade(_model.CurrentUpgradeLevel + 1))
                    {
                        UpdateUpgradeInfo();
                        _upgradeCompleteCallback?.Invoke();
                    }
                }
            }
        }

        public void OpenPopup(FactoryBuildingModel model, Action completeCallback = null)
        {
            _upgradeCompleteCallback = completeCallback;
            _model = model;

            _factoryPopup.panelTitle.text = $"{model.ProductType.ToString()} Factory";
            _factoryPopup.SetResourceIcon(model.ProductType);

            UpdateUpgradeInfo();

            _factoryPopup.Open();
        }


        private void UpdateUpgradeInfo()
        {
            _factoryPopup.currentInfoText.text =
                $"Current Level:{_model.CurrentUpgradeLevel + 1} production count:{_model.ProductionCount} per {_model.ProductionTime:f2} seconds.";

            var nextUpgrade = _model.NextUpgrade;

            if (nextUpgrade != null)
            {
                _factoryPopup.UpgradeButton.Enabled = _balanceModel.CanDebit(_model.CurrencyType, _model.CurrentPrice);
                _factoryPopup.nextUpgradeInfoText.text =
                    $"Upgrade Level:{nextUpgrade.Value.Index + 1}\n production count:{nextUpgrade.Value.ProductionCount} per {nextUpgrade.Value.ProductionTime:f2} seconds.";
                _factoryPopup.UpgradeButton.Label =
                    $"Upgrade {_model.CurrentPrice} <sprite name={_model.CurrencyType.ToString()}>";
            }
            else
            {
                _factoryPopup.nextUpgradeInfoText.text = "";
                _factoryPopup.UpgradeButton.Label = "MAX UPGRADES";
                _factoryPopup.UpgradeButton.Enabled = false;
            }
        }

        public void Dispose()
        {
            if (_factoryPopup != null)
            {
                _factoryPopup.UpgradeButton.OnClick -= OnUpgradeClickHandler;
            }
        }
    }
}