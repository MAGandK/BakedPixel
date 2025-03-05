using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Window.WinWindow
{
    public class WinWindowView : AbstractWindowView
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _rewardButton;
        
        public void SubscribeButton(UnityAction onContinueButtonClick, UnityAction onRewardButtonClick)
        {
            // _continueButton.onClick.AddListener(onContinueButtonClick);
            // _rewardButton.onClick.AddListener(onRewardButtonClick);
        }
    }
}