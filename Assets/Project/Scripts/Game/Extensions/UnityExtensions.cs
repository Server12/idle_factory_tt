using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Extensions
{
    public static class UnityExtensions
    {
        public static void DestroyChildrenImmediate(this Transform holder, List<GameObject> exludeList = null)
        {
            var list = holder.Cast<Transform>().ToList();
            foreach (Transform child in list)
            {
                if (exludeList != null && exludeList.Contains(child.gameObject)) continue;
                UnityEngine.Object.DestroyImmediate(child.gameObject);
            }
        }
        
        public static void DestroyChildren(this Transform holder, List<GameObject> exludeList = null)
        {
            var list = holder.Cast<Transform>().ToList();
            foreach (Transform child in list)
            {
                if (exludeList != null && exludeList.Contains(child.gameObject)) continue;
                UnityEngine.Object.Destroy(child.gameObject);
            }
        }

        public static void SnapUIToWorldPos(this RectTransform uiElement, Camera camera, Vector3 wPos,
            RectTransform canvasRect)
        {
            var fromScreenPoint1 = camera.WorldToScreenPoint(wPos);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect,
                fromScreenPoint1,
                camera,
                out Vector2 localPoint);


            uiElement.transform.localPosition = localPoint;
        }
    }
}