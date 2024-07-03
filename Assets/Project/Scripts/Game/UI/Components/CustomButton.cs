using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Components
{
    [RequireComponent(typeof(Button))]
    public class CustomButton : MonoBehaviour
    {
        public event Action OnClick;

        public event Action<int> OnInstanceClicked;

        [SerializeField] private Button _button;

        public virtual bool Enabled
        {
            get { return _button.interactable; }
            set { _button.interactable = value; }
        }

        protected virtual void OnValidate()
        {
            if (_button == null)
            {
                _button = GetComponent<Button>();
            }
        }

        // public object Data { get; set; }

        protected virtual void OnEnable()
        {
            _button.onClick.AddListener(OnClickHandler);
        }

        protected virtual void OnDisable()
        {
            _button.onClick.RemoveListener(OnClickHandler);
        }

        protected virtual void OnClickHandler()
        {
            OnClick?.Invoke();
            OnInstanceClicked?.Invoke(GetInstanceID());
        }
    }
}