using Game.UI.Popups;
using Game.UI.Screens;

namespace Game.UI.Managers
{
    public interface IUIFactory
    {
        T GetOrCreatePopup<T>() where T : BasePopupUIView;

        T GetOrCreateScreen<T>() where T : BaseUIScreenView;
    }
}