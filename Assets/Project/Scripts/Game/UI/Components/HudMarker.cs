using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Components
{
    [RequireComponent(typeof(Canvas))]
    public class HudMarker : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        [SerializeField] private Image icon;

        public Image disabledIcon;

        public void SetIcon(Sprite mainIcon)
        {
            icon.sprite = mainIcon;
            LookToCamera();
        }

        [Button]
        public void LookToCamera()
        {
            var cam = Camera.main;
            _canvas.worldCamera = cam;
            transform.LookAt(cam.transform, transform.up);
        }
    }
}