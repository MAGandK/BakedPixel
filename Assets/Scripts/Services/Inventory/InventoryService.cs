using System;
using System.Collections.Generic;
using System.Linq;
using Constants;
using Services.Storage;
using UnityEngine;
using Zenject;

namespace Services.Inventory
{
    public class InventoryService : IInventoryService, IInitializable
    {
        public event Action CellCountChanged;
        public event Action<Vector2Int> CellChanged;

        private readonly InventoryConfig _configs;
        private readonly InventoryData _inventoryData;

        private Dictionary<Vector2Int, InventoryItemData> _inventoryMap;

        public Dictionary<Vector2Int, InventoryItemData> InventoryMap => _inventoryMap;

        public InventoryService(InventoryConfig configs, IStorageService storageService)
        {
            _configs = configs;
            _inventoryData = storageService.GetData<InventoryData>(StorageDataNames.INVENTORY_DATA_KEY);
        }

        public void Initialize()
        {
            _inventoryMap = _inventoryData.InventoryItems.ToDictionary(x => x.Position, x => x);

            CellChanged += OnCellChanged;
        }

        private void SaveMap()
        {
            var inventoryItemDatas = _inventoryMap.Values.ToList();
            _inventoryData.SetNewInventoryItems(inventoryItemDatas);
        }

        public void AddItem(string id, int count)
        {
            Debug.Log($"Adding item: {id}, count: {count}");
            for (int i = 0; i < _configs.Row; i++)
            {
                for (int j = 0; j < _configs.Column; j++)
                {
                    var tempPosition = new Vector2Int(i, j);

                    if (!_inventoryMap.ContainsKey(tempPosition))
                    {
                        _inventoryMap.Add(tempPosition, new InventoryItemData(id, count, tempPosition));

                        CellChanged?.Invoke(tempPosition);

                        return;
                    }
                }
            }
        }

        public void AddItem(string id, Vector2Int position, int count)
        {
            if (_inventoryMap.TryGetValue(position, out var inventoryItemData))
            {
                if (inventoryItemData.ID != id)
                {
                    return;
                }

                inventoryItemData.AddCount(count);
            }

            _inventoryMap.Add(position, new InventoryItemData(id, count, position));

            CellChanged?.Invoke(position);
        }

        public void RemoveItem(string id)
        {
            var inventoryMap = _inventoryMap;

            foreach (var (key, inventoryItemData) in _inventoryMap)
            {
                if (inventoryItemData.ID == id)
                {
                    inventoryMap.Remove(key);
                    CellChanged?.Invoke(key);

                    break;
                }
            }

            _inventoryMap = inventoryMap;
        }

        public void RemoveItem(string id, Vector2Int position)
        {
            if (!_inventoryMap.ContainsKey(position))
            {
                return;
            }

            _inventoryMap.Remove(position);
            CellChanged?.Invoke(position);
        }

        public int GetMaxCellCount()
        {
            return _configs.Column * _configs.Row;
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

        public InventoryItemData GetData(Vector2Int position)
        {
            return _inventoryMap.GetValueOrDefault(position);
        }

        private void OnCellChanged(Vector2Int obj)
        {
            SaveMap();
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