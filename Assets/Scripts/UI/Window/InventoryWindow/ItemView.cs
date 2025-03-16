using System;
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
        //
        // private Vector3 _startPosition;
        // private float _offset = 20f;
        // private bool _isDragging = false;

        // private void Start()
        // {
        //     _startPosition = _itemImage.rectTransform.localPosition;
        // }

        public void Refresh(int count, Sprite sprite)
        {
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

        // public void OnPointerDown(PointerEventData eventData)
        // {
        //     Debug.Log("OnPointerDown: Палец на иконке!");
        //     _isDragging = true;
        //     _itemImage.rectTransform.localPosition += new Vector3(0, _offset, 0);
        // }
        //
        // public void OnPointerUp(PointerEventData eventData)
        // {
        //     Debug.Log("OnPointerUp: Палец убран!");
        //     _isDragging = false;
        //     _itemImage.rectTransform.localPosition = _startPosition;
        // }
       
    }
}