using System;
using System.Collections.Generic;
using Game.Configs.UI;
using Game.UI.Popups;
using Game.UI.Screens;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Game.UI.Managers
{
    public class UIFactory : IUIFactory
    {
        private readonly UIViewsSO _viewsSo;


        private readonly Dictionary<Type, BasePopupUIView> _popupsDict = new Dictionary<Type, BasePopupUIView>();
        private readonly Dictionary<Type, BaseUIScreenView> _screenDicts = new Dictionary<Type, BaseUIScreenView>();

        public UIFactory(UIViewsSO viewsSo)
        {
            _viewsSo = viewsSo;
        }

        public T GetOrCreatePopup<T>() where T : BasePopupUIView
        {
            var type = typeof(T);
            if (_popupsDict.TryGetValue(type, out var cachedPopup))
            {
                return (T)cachedPopup;
            }

            var popupPrefab = _viewsSo.GetPopupPrefab<T>();
            var popup = Object.Instantiate(popupPrefab);
            popup.OnDestroyed += OnPopupDestroyedHandler;
            popup.OnDisabled += OnPopupDisabledHandler;
            _popupsDict.TryAdd(type, popup);
            return (T)popup;
        }

        public T GetOrCreateScreen<T>() where T : BaseUIScreenView
        {
            var type = typeof(T);
            if (_screenDicts.TryGetValue(type, out var uiScreen))
            {
                return (T)uiScreen;
            }

            var screenPrefab = _viewsSo.GetScreenPrefab<T>();
            var screen = Object.Instantiate(screenPrefab);

            return screen;
        }

        private void OnPopupDisabledHandler(BasePopupUIView popup)
        {
            var type = popup.GetType();
            _popupsDict.TryAdd(type, popup);
        }

        private void OnPopupDestroyedHandler(BasePopupUIView popup)
        {
            popup.OnDisabled -= OnPopupDisabledHandler;
            popup.OnDestroyed -= OnPopupDestroyedHandler;
            var type = popup.GetType();
            if (_popupsDict.ContainsKey(type))
            {
                _popupsDict.Remove(type);
            }
        }
    }
}