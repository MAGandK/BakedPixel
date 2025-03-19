using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Window.InventoryWindow
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _textCount;

        [SerializeField] private TMP_Text _position;
        public void Refresh(int count, Sprite sprite, Vector2Int position)
        {
            _position.text = position.ToString();
            
            if (count == 0)
            {
               gameObject.SetActive(false);
               
               return;
            }
            else
            {
                _textCount.gameObject.SetActive(true);
                _textCount.text = count.ToString();
            }

            if (sprite == null)
            {
                _itemImage.gameObject.SetActive(false);
            }
            else
            {
                _itemImage.gameObject.SetActive(true);
                _itemImage.sprite = sprite;
            }
        }
    }
}