using Game.Extensions;
using Game.UI.Popups;
using Game.UI.Screens;
using UnityEngine;

namespace Game.Configs.UI
{
    [CreateAssetMenu(fileName = "UIViews", menuName = "Create/UIViews", order = 0)]
    public class UIViewsSO : ScriptableObject
    {
        [SerializeField] private BaseUIScreenView[] _screenPrefabs;
        [SerializeField] private BasePopupUIView[] _popupViews;
        
        
        public T GetPopupPrefab<T>() where T:BasePopupUIView
        {
            return (T)_popupViews.Find(ui => ui is T);
        }

        public T GetScreenPrefab<T>() where T : BaseUIScreenView
        {
            return (T)_screenPrefabs.Find(ui => ui is T);
        }
    }
}