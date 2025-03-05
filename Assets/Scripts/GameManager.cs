using System;
using Services.Storage;
using UI;
using UI.Window.FailWindow;
using UI.Window.GameWindow;
using UI.Window.StartWindow;
using UI.Window.WinWindow;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public event Action GameRestarted;
        public event Action GameFinished;
        public event Action GameStarted;

        public event Action GameExited;

       // private AudioManager _audioManager;
        private IUIController _uiController;
        private IStorageService _storageService;
        private StartWindowController _startWindow;
        private FailWindowController _failWindow;
        private GameWindowController _gameWindow;
        private WinWindowController _winWindow;
        private LevelProgressStorageData _levelProgressStorageData;

        [Inject]
        private void Construct( IUIController uiController, IStorageService storageService)
        {
         
            _uiController = uiController;
            _storageService = storageService;
        }

        private void Awake()
        {
            _startWindow = _uiController.GetWindow<StartWindowController>();
            _failWindow = _uiController.GetWindow<FailWindowController>();
            _gameWindow = _uiController.GetWindow<GameWindowController>();
            _winWindow = _uiController.GetWindow<WinWindowController>();

            LoadPlayerData();
        }

        private void LoadPlayerData()
        {
           
        }

        public void StartGame()
        {
            _uiController.ShowWindow<GameWindowController>();
           // _audioManager.PlayBackgroundMusic();
            GameStarted?.Invoke();
        }

        public void FinishGame()
        {
            _levelProgressStorageData.IncrementLevelIndex();
            _uiController.ShowWindow<WinWindowController>();
            //_audioManager.StopMusic();
            //_audioManager.PlaySound(SoundType.Finish);
            GameFinished?.Invoke();
        }

        public void RestartGame()
        {
            _uiController.ShowWindow<GameWindowController>();
       
            GameRestarted?.Invoke();
            StartGame();
        }

        public void ExitGame()
        {
            _uiController.ShowWindow<StartWindowController>();
          
            GameExited?.Invoke();
        }


        private void StartWindowOnStartButtonPressed()
        {
            StartGame();
        }

        private void FailWindowViewOnNoTryButtonPressed()
        {
            ExitGame();
        }

        private void FailWindowViewOnRetryButtonPressed()
        {
            RestartGame();
        }


#if UNITY_EDITOR

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                FinishGame();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                RestartGame();
            }
        }
#endif
    }
}