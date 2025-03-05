using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Window.FailWindow
{
    public class FailWindowView : AbstractWindowView
    {
        [SerializeField] private Button _retryButton;
        [SerializeField] private Button _noTryButton;
       
        public void SubscribeButton(UnityAction onRetryClick, UnityAction onTryClick)
        {
            // _retryButton.onClick.AddListener(onRetryClick);
            // _noTryButton.onClick.AddListener(onTryClick);
        }
    }
}