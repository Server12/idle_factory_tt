using System;
using Game.Configs.UI;
using Game.Map.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI.Components
{
    public class RecipePanel : MonoBehaviour, IPointerClickHandler
    {
        public event Action<RecipePanel> OnPanelClicked;

        private ToolsCraftData _craftData;

        [SerializeField] private UIIconsSO _iconsSo;
        [SerializeField] private Image leftItemIcon;
        [SerializeField] private Image rightItemIcon;
        [SerializeField] private Image resultItemIcon;
        public Image checkMark;

        public ToolsCraftData CraftData => _craftData;


        public void SetCraftData(ToolsCraftData craftData)
        {
            _craftData = craftData;
            leftItemIcon.sprite = _iconsSo.GetResourceItemIcon(craftData.Item1);
            rightItemIcon.sprite = _iconsSo.GetResourceItemIcon(craftData.Item2);
            resultItemIcon.sprite = _iconsSo.GetResourceItemIcon(craftData.CraftedItem);
        }


        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            checkMark.enabled = true;
            OnPanelClicked?.Invoke(this);
        }
    }
}