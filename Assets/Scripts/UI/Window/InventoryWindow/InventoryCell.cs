using System;
using Services.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Window.InventoryWindow
{
    public class InventoryCell : MonoBehaviour, IPointerDownHandler, IDropHandler
    {
        public event Action<Vector2Int> PointerDown;
        public event Action<Vector2Int> Droped;

        [SerializeField] private Image _back;
        [SerializeField] private ItemView _itemView;
        [SerializeField] private Color _lockColor;
        [SerializeField] private Color _unlockColor;

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

        public void Setup(Vector2Int position)
        {
            HideContent();
            Position = position;
        }

        public void SetIsLock(bool isLock)
        {
            _back.color = isLock ? _lockColor : _unlockColor;
        }

        public void Refresh(int count, Sprite sprite)
        {
            _itemView.gameObject.SetActive(true);
            _itemView.Refresh(count, sprite);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_itemView.gameObject.activeSelf)
            {
                PointerDown?.Invoke(Position);
            }

            //  _originalPosition = _rectTransform.anchoredPosition;
            // _canvasGroup.alpha = 0.6f;
        }

        public void OnDrop(PointerEventData eventData)
        {
           Droped?.Invoke(Position);
        }

        public void HideContent()
        {
            _itemView.gameObject.SetActive(false);
        }
    }
}