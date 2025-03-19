using System;
using System.Collections.Generic;
using Services.Inventory;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Window.InventoryWindow
{
    public class InventoryWindowView : AbstractWindowView, IDropHandler
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private InventoryCell _inventoryCellPrefab;
        [SerializeField] private ItemView _inventoryItemViewPrefab;
        [SerializeField] private Transform _cellParent;
        [SerializeField] private Button _addItemButton;
        [SerializeField] private Button _deleteButton;
        [SerializeField] private ItemView _movebleItemView;
        [SerializeField] private Vector3 _movableOffset;
        [Space]
        [SerializeField] private Vector2Int _position;
        [SerializeField] private int _count;
        
        private InventoryCell[,] _cells;
        private Action _onDropOutOfCell;

        private void Update()
        {
            _movebleItemView.transform.position = Input.mousePosition + _movableOffset;
        }

        public void PrepareCells(int rows, int columns, int unlockCellCount)
        {
            _cells = new InventoryCell[rows, columns];

            int cellIndex = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    _cells[i, j] = Instantiate(_inventoryCellPrefab, _cellParent);

                    _cells[i, j].SetIsLock(cellIndex >= unlockCellCount);
                    _cells[i, j].Setup(new Vector2Int(i,j));

                    cellIndex++;
                }
            }
        }

        public void SubckribeOnCells(Action<Vector2Int> onCellPointerDown, Action<Vector2Int> onDrop, Action<Vector2Int> onCellPointerUp)
        {
            foreach (var cell in _cells)
            {
                cell.Droped += onDrop;
                cell.PointerDown += onCellPointerDown;
                cell.PointerUp += onCellPointerUp;
            }
        }
        
        public void UnsubckribeOnCells(Action<Vector2Int> onCellPointerDown, Action<Vector2Int> onDrop,Action<Vector2Int> onCellPointerUp)
        {
            foreach (var cell in _cells)
            {
                cell.Droped -= onDrop;
                cell.PointerDown -= onCellPointerDown;
                cell.PointerUp -= onCellPointerUp;
            }
        }
        public void FillCells(Dictionary<Vector2Int, InventoryItemData> inventoryItems, Func<string, Sprite> getSprite)
        {
            foreach (var (position, data) in inventoryItems)
            {
                _cells[position.x, position.y].Refresh(data.Count, getSprite(data.ID), position);
            }
        }

        public void SubscribeButton(Action<int, Vector2Int> onAddItemButtonClick, UnityAction onDeleteButtonClick, Action onDropOutOfCell)
        {
            _onDropOutOfCell = onDropOutOfCell;
            _addItemButton.onClick.AddListener(()=>onAddItemButtonClick?.Invoke(_count,_position));
            _deleteButton.onClick.AddListener(onDeleteButtonClick);
        }

        public void RefreshCell(Vector2Int position, int count, Sprite spite)
        {
            _cells[position.x, position.y].Refresh(count, spite, position);
        }

        public void HideCell(Vector2Int position)
        {
            _cells[position.x, position.y].HideContent();
        }

        public void SetupMovebleView(Vector2Int position, int count, Sprite sprite)
        {
            _movebleItemView.gameObject.SetActive(true);
            _movebleItemView.Refresh(count, sprite, position);
            _movebleItemView.transform.position = _cells[position.x, position.y].transform.position;
        }
        
        public void ActivateScroller()
        {
            _scrollRect.vertical = true;
        }
        public void DisableScroller()
        {
            _scrollRect.vertical = false;
        }

        public void HideMovable()
        {
            _movebleItemView.gameObject.SetActive(false);
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            _onDropOutOfCell?.Invoke();
        }
    }
}