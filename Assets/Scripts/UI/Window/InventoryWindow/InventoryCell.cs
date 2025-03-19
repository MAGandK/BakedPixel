using System;
using Services.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Window.InventoryWindow
{
    public class InventoryCell : MonoBehaviour, IPointerDownHandler, IDropHandler, IPointerUpHandler
    {
        public event Action<Vector2Int> PointerDown;
        public event Action<Vector2Int> PointerUp;
        public event Action<Vector2Int> Droped;

        [SerializeField] private Image _back;
        [SerializeField] private ItemView _itemView;
        [SerializeField] private Color _lockColor;
        [SerializeField] private Color _unlockColor;
        
        private InventoryService _inventoryService;
        private InventoryItemData _currentItemData;
        private Vector2 _originalPosition;
        public Vector2Int Position { get; private set; }

        public void Setup(Vector2Int position)
        {
            HideContent();
            Position = position;
        }

        public void SetIsLock(bool isLock)
        {
            _back.color = isLock ? _lockColor : _unlockColor;
        }

        public void Refresh(int count, Sprite sprite, Vector2Int position)
        {
            _itemView.gameObject.SetActive(true);
            _itemView.Refresh(count, sprite, position);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_itemView.gameObject.activeSelf)
            {
                PointerDown?.Invoke(Position);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
           Droped?.Invoke(Position);
        }

        public void HideContent()
        {
            _itemView.gameObject.SetActive(false);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PointerUp?.Invoke(Position);
        }
    }
}