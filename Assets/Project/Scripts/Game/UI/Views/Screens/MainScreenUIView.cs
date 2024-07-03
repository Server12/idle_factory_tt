using System;
using System.Collections.Generic;
using Game.Configs.UI;
using Game.Data;
using Game.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Screens
{
    public class MainScreenUIView : BaseUIScreenView
    {
        [SerializeField] private UIIconsSO uiIconsSo;
        [SerializeField] private GridLayoutGroup _gridLayout;
        [SerializeField] private ResourcePanel _panelInstance;

        private readonly Dictionary<CurrencyType, ResourcePanel> _currencyPanels =
            new Dictionary<CurrencyType, ResourcePanel>();


        private readonly Dictionary<ResourceItemType, ResourcePanel> _resourcePanels =
            new Dictionary<ResourceItemType, ResourcePanel>();

        public TasksButton tasksButton;
        public LabelButton nextLevelButton;


        private void Start()
        {
            _panelInstance.gameObject.SetActive(false);
        }

        public void CreateOrUpdateCurrencyPanel(CurrencyType currencyType, int value)
        {
            InternalCreateOrUpdatePanel(currencyType, _currencyPanels, value);
        }


        public void CreateOrUpdateResourcePanel(ResourceItemType itemType, int value)
        {
            InternalCreateOrUpdatePanel(itemType, _resourcePanels, value);
        }


        private void InternalCreateOrUpdatePanel<TEnum, TDict>(TEnum type, TDict dict, int updateValue)
            where TEnum : struct, Enum where TDict : IDictionary<TEnum, ResourcePanel>
        {
            if (dict.TryGetValue(type, out var panel))
            {
                panel.SetValue(updateValue);
            }
            else
            {
                panel = Instantiate(_panelInstance, _gridLayout.transform);
                panel.gameObject.SetActive(true);
                panel.Icon.sprite = uiIconsSo.GetIcon(type);
                panel.SetValue(updateValue);
                dict.Add(type, panel);
            }
        }
    }
}