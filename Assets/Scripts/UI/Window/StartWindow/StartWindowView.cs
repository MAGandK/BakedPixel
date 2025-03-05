using System;
using UI.Other;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Window.StartWindow
{
    public class StartWindowView : AbstractWindowView
    {
        [SerializeField] private LevelProgressBar _levelProgressBar;
        [SerializeField] private Button _startButton;
        [SerializeField] private BalanceView _balanceView;
        [SerializeField] private Button _openInventoryButton;
        public BalanceView BalanceView => _balanceView;

        public void SubscribeButtons(UnityAction onStartButtonClick, UnityAction onInventoryButtonClick)
        {
            _startButton.onClick.AddListener(onStartButtonClick);
            _openInventoryButton.onClick.AddListener(onInventoryButtonClick);
        }

        public void SetupProgressBar(int levelIndex)
        {
            _levelProgressBar.Setup(levelIndex);
        }
        
    }
}