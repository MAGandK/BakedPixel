using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Window.InventoryWindow
{
    public class InventoryWindowView : AbstractWindowView
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private InventoryCell _inventoryCellPrefab;
        [SerializeField] private InventoryItemView _inventoryItemViewPrefab;
        [SerializeField] private Transform _cellParent;
        [SerializeField] private Button _addItemButton;
        [SerializeField] private Button _deleteButton;
        
        
        private InventoryCell[,] _cells; 
        public void PrepareCells(int rows, int columns, int unlockCellCount)
        {
            _cells = new InventoryCell[rows, columns];  

            int cellIndex = 0;
        
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    _cells[i, j] = Instantiate(_inventoryCellPrefab, _cellParent);

                    _cells[i, j].Setup(cellIndex >= unlockCellCount);
                
                    cellIndex++;
                }
            }
        }
        
        public void SubscribeButton(UnityAction onAddItemButtonClick, UnityAction onDeleteButtonClick)
        {
            _addItemButton.onClick.AddListener(onAddItemButtonClick);
            _deleteButton.onClick.AddListener(onDeleteButtonClick);
        }
    }
}