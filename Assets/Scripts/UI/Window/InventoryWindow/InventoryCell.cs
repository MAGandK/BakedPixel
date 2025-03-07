using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Window.InventoryWindow
{
    public class InventoryCell : MonoBehaviour, IPointerClickHandler
    {
        private event Action Clicked;

        [SerializeField] private Image _back;
        [SerializeField] private Color _lockColor;
        [SerializeField] private Color _unlockColor;
        [SerializeField] private TMP_Text _textCount;
        [SerializeField] private Image _itemImage;

        public void Setup(bool isLock, int count, Sprite sprite)
        {
            SetIsLock(isLock);
            Refresh(count, sprite);
        }

        public void SetIsLock(bool isLock)
        {
            _back.color = isLock ? _lockColor : _unlockColor;
        }

        public void Refresh(int count, Sprite sprite)
        {
            Debug.Log($"Refreshing cell: count={count}, sprite={sprite?.name}");
            if (count == 0)
            {
                _textCount.gameObject.SetActive(false);
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

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke();
        }
    }
}