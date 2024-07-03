using System;
using Game.Map.Data;
using Game.UI.Components;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Map.Views.Buildings
{
    public class BuildingView : MonoBehaviour, IPointerClickHandler
    {
        public event Action<int> OnClicked;

        [ReadOnly] [SerializeField] private int _instanceId;

        [SerializeField] private Vector3 offset;
        public HudMarker HudMarker;

        public int InstanceId
        {
            get
            {
                _instanceId = GetInstanceID();
                return _instanceId;
            }
        }

        public void SetPosition(Vector3 pos)
        {
            transform.position = pos + offset;
        }

        private void OnValidate()
        {
            if (HudMarker == null)
            {
                HudMarker = GetComponentInChildren<HudMarker>();
            }
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke(InstanceId);
        }
    }
}