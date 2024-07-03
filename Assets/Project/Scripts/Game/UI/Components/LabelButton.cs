using TMPro;
using UnityEngine;

namespace Game.UI.Components
{
   
    public class LabelButton : CustomButton
    {
        
        [SerializeField] private TMP_Text _label;

        protected override void OnValidate()
        {
            base.OnValidate();
            if (_label == null)
            {
                _label = GetComponentInChildren<TMP_Text>();
            } 
        }
        
        public string Label
        {
            get => _label?.text ?? "";
            set
            {
                if (_label != null)
                {
                    _label.text = value;
                }
            }
        }
    }
}