using System;
using Game.Map.Models;

namespace Game.UI.Presenters
{
    public interface IPopupPresenter<T> where T:IBuildingModel
    {
        void OpenPopup(T model, Action completeCallback = null);
    }
}