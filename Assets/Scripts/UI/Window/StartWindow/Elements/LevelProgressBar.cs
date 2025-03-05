using TMPro;
using UnityEngine;

namespace UI.Window.StartWindow
{
    public class LevelProgressBar : MonoBehaviour
    {
        [SerializeField] private TMP_Text _startLevelText;
        [SerializeField] private TMP_Text _endLevelText;
        [SerializeField] private Transform _bonusLevelPoint;
        [SerializeField] private LevelProgressBarElement[] _elements;

        [SerializeField] private Color _activeLevelColor;
        [SerializeField] private Color _prevLeveColor;
        [SerializeField] private Color _nextLevelColor;

        public void Setup(int currentLevel)
        {
            var level = currentLevel /10;
            var startLevel = level  * 10;
            var endLevel = startLevel + 10;

            _startLevelText.text = (startLevel + 1).ToString();
            _endLevelText.text = endLevel.ToString();

            int levelIndex = (currentLevel + 1) % 10 ;
            
            if (levelIndex == 0)
            {
                levelIndex = _elements.Length;
            }
            
            for (int i = 0; i < _elements.Length; i++)
            {
               _elements[i].SetColor(GetColorElement(i+ 1, levelIndex));
            }
        }

        private Color GetColorElement(int index, int currentLevelIndex)
        {
            if (index == currentLevelIndex)
            {
                return _activeLevelColor;
            }

            if (index > currentLevelIndex)
            {
                return _nextLevelColor;
            }

            return _prevLeveColor;
        }
    }
}
        
    