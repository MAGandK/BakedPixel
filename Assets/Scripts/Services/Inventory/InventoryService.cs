using System;
using Constants;
using Services.Storage;
using UnityEngine;

namespace Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        public event Action CellCountChanged;
        public event Action<string> IdChanged;
        public event Action<int> ItemCountChanged;
        public event Action<string, int> ItemsAdded;
        public event Action<string, int> ItemsRemoved;
        
        public string Id { get; }
        public int Cont { get; }
        public bool IsEmpty { get; }

        private InventoryConfig _configs;
        private InventoryData _inventoryData;

        private InventoryItemConfig[,] _inventory;

        public InventoryService(InventoryConfig configs, IStorageService storageService)
        {
            _configs = configs;
            _inventoryData = storageService.GetData<InventoryData>(StorageDataNames.INVENTORY_DATA_KEY);
            _inventory = new InventoryItemConfig[configs.MaxCellLine, configs.CellLineItemCount];
        }

        public void AddItem(string id)
        {
            for (int i = 0; i < _inventory.GetLength(0); i++)
            {
                for (int j = 0; j < _inventory.GetLength(1); j++)
                {
                    if (_inventory[i, j] == null)
                    {
                        InventoryItemConfig itemConfig = FindItemConfigById(id);

                        if (itemConfig != null)
                        {
                            _inventory[i, j] = itemConfig;
                            return;
                        }

                        return;
                    }
                }
            }
        }

        public void AddItem(string id, Vector2Int position)
        {
        }

        public void RemoveItem(string id)
        {
            for (int i = 0; i < _inventory.GetLength(0); i++)
            {
                for (int j = 0; j < _inventory.GetLength(1); j++)
                {
                    if (_inventory[i, j] != null && _inventory[i, j].Id == id)
                    {
                        _inventory[i, j] = null;

                        CellCountChanged?.Invoke();
                        return;
                    }
                }
            }
        }

        public void RemoveItem(string id, Vector2Int position)
        {
        }

        public int GetMaxCellCount()
        {
            return _configs.MaxCellLine;
        }

        public int GetUnlockCellCount()
        {
            return _configs.StartLockCellCount + _inventoryData.AdditionalCellCount;
        }

        public void UnlockCell()
        {
            if (GetUnlockCellCount() >= GetMaxCellCount())
            {
                return;
            }

            _inventoryData.UnlockCell();

            CellCountChanged?.Invoke();
        }

        private InventoryItemConfig FindItemConfigById(string id)
        {
            foreach (var itemConfig in _configs.ItemConfigs)
            {
                if (itemConfig.Id == id)
                {
                    return itemConfig;
                }
            }

            return null;
        }
    }
}