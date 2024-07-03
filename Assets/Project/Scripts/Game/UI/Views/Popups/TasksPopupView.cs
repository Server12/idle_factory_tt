using Game.UI.Components;

namespace Game.UI.Popups
{
    public class TasksPopupView : BasePopupUIView
    {
        public TaskItemPanel taskPanelInstance;
        
        
        public override void Open()
        {
            gameObject.SetActive(true);
        }

        public override void Close()
        {
            gameObject.SetActive(false);
        }
    }
}