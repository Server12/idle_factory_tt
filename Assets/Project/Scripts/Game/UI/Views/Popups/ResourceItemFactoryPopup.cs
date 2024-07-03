using Game.Configs.UI;
using Game.Data;
using Game.UI.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Popups
{
    public class ResourceItemFactoryPopup : BasePopupUIView
    {
        [SerializeField] private UIIconsSO iconsSo;
        [SerializeField] private Image _resourceIcon;
        public TMP_Text currentInfoText;
        public TMP_Text nextUpgradeInfoText;
        public LabelButton UpgradeButton;


        public void SetResourceIcon(ResourceItemType itemType)
        {
            _resourceIcon.sprite = iconsSo.GetResourceItemIcon(itemType);
        }
        

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