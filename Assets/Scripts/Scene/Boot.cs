using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private string _uiSceneName;
        [SerializeField] private string _gameSceneName;

        private void Awake()
        {

            StartCoroutine(LoadSceneCor());

        }

        private IEnumerator LoadSceneCor()
        {
            yield return  SceneManager.LoadSceneAsync(_uiSceneName, LoadSceneMode.Additive);
        
            yield return SceneManager.LoadSceneAsync(_gameSceneName, LoadSceneMode.Additive);

            var sceneByName = SceneManager.GetSceneByName("GameScene");
            SceneManager.SetActiveScene(sceneByName);
         
            yield return SceneManager.UnloadSceneAsync(0);
        }
    }
}