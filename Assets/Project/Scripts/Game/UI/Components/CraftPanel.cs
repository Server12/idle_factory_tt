using Game.Configs.UI;
using Game.Map.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Components
{
    public class CraftPanel : MonoBehaviour
    {
        [SerializeField] private UIIconsSO _iconsSo;
        [SerializeField] private Image _leftItem;
        [SerializeField] private Image _rightItem;
        [SerializeField] private Image _result;

        [SerializeField] private TMP_Text _leftCounterText;
        [SerializeField] private TMP_Text _rightCounterText;
        [SerializeField] private TMP_Text _resultCounterText;

        public void SetCraftData(ToolsCraftData craftData)
        {
            gameObject.SetActive(true);
            UpdateCounters(0, 0, 0);
            _leftItem.sprite = _iconsSo.GetResourceItemIcon(craftData.Item1);
            _rightItem.sprite = _iconsSo.GetResourceItemIcon(craftData.Item2);
            _result.sprite = _iconsSo.GetResourceItemIcon(craftData.CraftedItem);
        }

        public void UpdateCounters(int leftCounter, int rightCounter, int craftedCount)
        {
            _leftCounterText.text = leftCounter.ToString();
            _resultCounterText.text = craftedCount.ToString();
            _rightCounterText.text = rightCounter.ToString();
        }
    }
}