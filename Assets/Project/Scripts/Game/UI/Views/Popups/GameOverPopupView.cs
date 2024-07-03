using Game.UI.Components;

namespace Game.UI.Popups
{
    public class GameOverPopupView : BasePopupUIView
    {
        public LabelButton restartButton;
        
        public override void Open()
        {
            gameObject.SetActive(true);
        }

        public override void Close()
        {
            Destroy(gameObject);
        }
    }
}