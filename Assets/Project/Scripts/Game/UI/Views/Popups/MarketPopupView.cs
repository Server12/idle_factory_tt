using Game.UI.Components;
using UnityEngine;

namespace Game.UI.Popups
{
    public class MarketPopupView : BasePopupUIView
    {
        [SerializeField] private SellItemPanel _panelInstance;

        public SellItemPanel PanelInstance => _panelInstance;


        public override void Open()
        {
            gameObject.SetActive(true);
        }

        public override void Close()
        {
            gameObject.SetActive(false);
        }
    }
}