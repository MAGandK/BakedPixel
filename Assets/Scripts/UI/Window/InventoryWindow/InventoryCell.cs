using System;
using Services.Inventory;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Window.InventoryWindow
{
    public class InventoryCell : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IDropHandler
    {
        private event Action Clicked;

        [SerializeField] private Image _back;
        [SerializeField] private Color _lockColor;
        [SerializeField] private Color _unlockColor;
        [SerializeField] private TMP_Text _textCount;
        [SerializeField] private Image _itemImage;

        private RectTransform _rectTransform;
        private InventoryService _inventoryService;
        private InventoryItemData _currentItemData;
        private CanvasGroup _canvasGroup;
        private Canvas _canvas;
        
        private Vector2 _originalPosition;

        public Vector2Int Position { get; private set; }
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvas = GetComponentInParent<Canvas>();
        }
        public void Setup(bool isLock, int count, Sprite sprite,Vector2Int position, InventoryItemData itemData)
        {
            SetIsLock(isLock);
            Refresh(count, sprite);
            Position = position;
            _currentItemData = itemData;
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
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _originalPosition = _rectTransform.anchoredPosition;
           // _itemImage.gameObject.SetActive(false);
           _canvasGroup.alpha = 0.6f;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            var dropPosition = eventData.position;
            Vector2Int targetPosition = new Vector2Int(Mathf.FloorToInt(dropPosition.x / _rectTransform.rect.width), 
                                                        Mathf.FloorToInt(dropPosition.y / _rectTransform.rect.height));
            _inventoryService.AddItem(_currentItemData.ID, targetPosition, _currentItemData.Count);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }


        public void OnDrop(PointerEventData eventData)
        {
           
        }
    }
}