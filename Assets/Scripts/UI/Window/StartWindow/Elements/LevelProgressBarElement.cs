using UnityEngine;
using UnityEngine.UI;

namespace UI.Window.StartWindow
{
    public class LevelProgressBarElement : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Image _conector;

        public void SetColor(Color color)
        {
            _image.color = color;
        }
    }
}