using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Configs.UI;
using Game.Map.Data;
using Game.Map.Models;
using Game.UI.Components;
using Game.UI.Managers;
using Game.UI.Popups;
using Object = UnityEngine.Object;

namespace Game.UI.Presenters
{
    public class MarketPopupPresenter : IPopupPresenter<IMarketBuildingModel>, IDisposable
    {
        private readonly IUIFactory _factory;
        private readonly UIIconsSO _iconsSo;

        private MarketPopupView _popupView;
        private IMarketBuildingModel _model;

        private Action _completeCallback;

        private readonly Dictionary<int, MarketSaleItem> _sellButtonTypes;
        private readonly List<SellItemPanel> _itemUIPanels;

        private CancellationTokenSource _tokenSource;

        public MarketPopupPresenter(IUIFactory factory, UIIconsSO iconsSo)
        {
            _factory = factory;
            _iconsSo = iconsSo;
            _sellButtonTypes = new Dictionary<int, MarketSaleItem>();
            _itemUIPanels = new List<SellItemPanel>();
        }

        public void OpenPopup(IMarketBuildingModel model, Action completeCallback = null)
        {
            _model = model;
            _completeCallback = completeCallback;

            if (_popupView == null)
            {
                _popupView = _factory.GetOrCreatePopup<MarketPopupView>();
                _popupView.panelCloseButton.OnClick += OnClosedHandler;

                var saleItemPanelPrefab = _popupView.PanelInstance;
                saleItemPanelPrefab.gameObject.SetActive(false);
                foreach (var saleItem in _model.SaleItems)
                {
                    var saleItemPanel = Object.Instantiate(saleItemPanelPrefab, _popupView.panelContent);

                    saleItemPanel.viewIndex = saleItemPanel.sellButton.GetInstanceID();
                    saleItemPanel.priceLabel.text = $"{saleItem.Price.Count} <sprite name={saleItem.Price.ItemType}>";
                    saleItemPanel.resourceIcon.sprite = _iconsSo.GetResourceItemIcon(saleItem.ResourceItemType);
                    saleItemPanel.resourceCountLabel.text = _model.GetSalesCount(saleItem.ResourceItemType).ToString();
                    saleItemPanel.sellButton.Enabled = _model.CanSale(saleItem);
                    saleItemPanel.sellButton.OnInstanceClicked += OnSellItemsClickedHandler;

                    saleItemPanel.gameObject.SetActive(true);

                    _sellButtonTypes.Add(saleItemPanel.viewIndex, saleItem);
                    _itemUIPanels.Add(saleItemPanel);
                }
            }

            _popupView.Open();

            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
            _tokenSource = CancellationTokenSource.CreateLinkedTokenSource(_popupView.destroyCancellationToken);

            StartUpdateCountersAsync(_tokenSource.Token).Forget();
        }

        private void OnClosedHandler()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
            _tokenSource = null;
        }

        private async UniTaskVoid StartUpdateCountersAsync(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await UniTask.DelayFrame(30, PlayerLoopTiming.Update, token);
                foreach (var itemUIPanel in _itemUIPanels)
                {
                    var saleItem = _sellButtonTypes[itemUIPanel.viewIndex];
                    itemUIPanel.resourceCountLabel.text =
                        _model.GetSalesCount(saleItem.ResourceItemType).ToString();
                    itemUIPanel.sellButton.Enabled = _model.CanSale(saleItem);
                }
            }
        }

        private void OnSellItemsClickedHandler(int instanceId)
        {
            var saleItem = _sellButtonTypes[instanceId];
            if (_model.Sale(saleItem))
            {
                _completeCallback?.Invoke();
            }
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
            
            if (_popupView != null)
            {
                _popupView.panelCloseButton.OnClick -= OnClosedHandler;
            }

            foreach (var sellItemPanel in _itemUIPanels)
            {
                sellItemPanel.sellButton.OnInstanceClicked -= OnSellItemsClickedHandler;
            }
        }
    }
}