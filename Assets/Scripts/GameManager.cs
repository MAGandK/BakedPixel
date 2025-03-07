using System;
using Services.Storage;
using UI;
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
        
        private IUIController _uiController;
        private IStorageService _storageService;

        private LevelProgressStorageData _levelProgressStorageData;

        [Inject]
        private void Construct( IUIController uiController, IStorageService storageService)
        {
         
            _uiController = uiController;
            _storageService = storageService;
        }

        private void Awake()
        {
            LoadPlayerData();
        }

        private void LoadPlayerData()
        {
           
        }

        public void StartGame()
        {
            GameStarted?.Invoke();
        }

        public void FinishGame()
        {
            _levelProgressStorageData.IncrementLevelIndex();
            GameFinished?.Invoke();
        }

        public void RestartGame()
        {
            GameRestarted?.Invoke();
            StartGame();
        }

        public void ExitGame()
        {
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

      
#endif
    }
}