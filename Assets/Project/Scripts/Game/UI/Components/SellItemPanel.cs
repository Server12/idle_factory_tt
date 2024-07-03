using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Components
{
    public class SellItemPanel : MonoBehaviour
    {
        [HideInInspector] public int viewIndex;
        public TMP_Text priceLabel;
        public LabelButton sellButton;
        public Image resourceIcon;
        public TMP_Text resourceCountLabel;
    }
}