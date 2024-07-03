using Game.UI.Components;
using UnityEngine;

namespace Game.UI.Popups
{
    public class CraftPopupView : BasePopupUIView
    {
        public RecipePanel RecipePanelInstance;
        public CraftPanel CraftPanel;
        public LabelButton startButton;

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