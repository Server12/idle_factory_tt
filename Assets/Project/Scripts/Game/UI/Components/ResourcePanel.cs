using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Components
{
    public class ResourcePanel : MonoBehaviour
    {
        public Image Icon;
        [SerializeField] private TextMeshProUGUI _label;

        private void OnEnable()
        {
            _label.text = "";
        }

        public void SetValue(int count)
        {
            _label.text = count.ToString();
        }

        // private void Start()
        // {
        //     if (ValueProperty != null)
        //         StartReadValueAsync(this.GetCancellationTokenOnDestroy()).Forget();
        // }
        //
        // private async UniTaskVoid StartReadValueAsync(CancellationToken cancellationToken)
        // {
        //     while (!cancellationToken.IsCancellationRequested)
        //     {
        //         var value = await ValueProperty.WaitAsync(cancellationToken);
        //         _label.text = value.ToString();
        //     }
        // }
    }
}