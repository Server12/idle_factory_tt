using System;
using Game.UI.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Popups
{
    public abstract class BasePopupUIView : MonoBehaviour
    {
        public event Action<BasePopupUIView> OnDestroyed;

        public event Action<BasePopupUIView> OnDisabled;
        
        public Canvas Canvas;
        public Image BlackVeil;
        public RectTransform Content;

        public RectTransform panelContent;
        public TextMeshProUGUI panelTitle;
        public CustomButton panelCloseButton;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnDisable()
        {
            OnDisabled?.Invoke(this);
            panelCloseButton.OnClick -= Close;
        }

        protected virtual void OnEnable()
        {
            panelCloseButton.OnClick += Close;
        }

        public abstract void Open();

        public abstract void Close();

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
            OnDestroyInternal();
        }

        protected virtual void OnDestroyInternal()
        {
        }
    }
}