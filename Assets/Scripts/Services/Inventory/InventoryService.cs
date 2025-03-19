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

        public AddInventoryItemResult AddItem(string id, int count)
        {
            var itemConfig = FindItemConfigById(id);

            var itemConfigMaxCount = itemConfig.MaxCount;

            if (TryAddToFilledCell(id, count, itemConfigMaxCount, out var result))
            {
                return result;
            }

            TryToPlaceFirstFreeCell(id, count, itemConfigMaxCount, out result);
            return result;
        }

        private void TryToPlaceFirstFreeCell(string id, int count, int itemConfigMaxCount,
            out AddInventoryItemResult result)
        {
            for (int i = 0; i < _configs.Row; i++)
            {
                for (int j = 0; j < _configs.Column; j++)
                {
                    var tempPosition = new Vector2Int(i, j);

                    if (!_inventoryMap.ContainsKey(tempPosition))
                    {
                        if (count <= itemConfigMaxCount)
                        {
                            result = new AddInventoryItemResult(true, 0);
                            _inventoryMap.Add(tempPosition, new InventoryItemData(id, count, tempPosition));
                        }
                        else
                        {
                            var remainingCount = count - itemConfigMaxCount;
                            _inventoryMap.Add(tempPosition,
                                new InventoryItemData(id, itemConfigMaxCount, tempPosition));
                            result = new AddInventoryItemResult(true, remainingCount);
                        }

                        CellChanged?.Invoke(tempPosition);

                        return;
                    }
                }
            }

            result = new AddInventoryItemResult(false, 0);
        }

        private bool TryAddToFilledCell(string id, int count, int itemConfigMaxCount, out AddInventoryItemResult result)
        {
            foreach (var (position, inventoryItemData) in _inventoryMap)
            {
                if (inventoryItemData.ID == id && inventoryItemData.Count < itemConfigMaxCount)
                {
                    var expectedCount = inventoryItemData.Count + count;

                    if (expectedCount > itemConfigMaxCount)
                    {
                        var remainingCount = expectedCount - itemConfigMaxCount;
                        inventoryItemData.SetCount(itemConfigMaxCount);

                        result = new AddInventoryItemResult(true, remainingCount);
                    }
                    else
                    {
                        inventoryItemData.SetCount(expectedCount);
                        result = new AddInventoryItemResult(true, 0);
                    }

                    CellChanged?.Invoke(position);

                    return true;
                }
            }

            result = new AddInventoryItemResult(false, 0);

            return false;
        }

        public AddInventoryItemResult AddItem(string id, Vector2Int position, int count)
        {
            if (!IsCorrectPosition(position))
            {
                return new AddInventoryItemResult(false, 0);
            }

            var itemConfigMaxCount = FindItemConfigById(id).MaxCount;

            if (_inventoryMap.TryGetValue(position, out var inventoryItemData))
            {
                if (inventoryItemData.ID != id)
                {
                    return new AddInventoryItemResult(false, 0);
                }

                var expectedPlacedCount = inventoryItemData.Count + count;

                if (expectedPlacedCount <= itemConfigMaxCount)
                {
                    inventoryItemData.SetCount(inventoryItemData.Count + count);
                    CellChanged?.Invoke(position);
                    
                    return new AddInventoryItemResult(true, 0);
                }

                inventoryItemData.SetCount(itemConfigMaxCount);
                CellChanged?.Invoke(position);

                return new AddInventoryItemResult(true, expectedPlacedCount - itemConfigMaxCount);
            }

            if (count <= itemConfigMaxCount)
            {
                _inventoryMap.Add(position, new InventoryItemData(id, count, position));
                CellChanged?.Invoke(position);

                return new AddInventoryItemResult(true, 0);
            }
            
            _inventoryMap.Add(position, new InventoryItemData(id, itemConfigMaxCount, position));
            CellChanged?.Invoke(position);
            
            return new AddInventoryItemResult(true, count - itemConfigMaxCount);
        }

        private bool IsCorrectPosition(Vector2Int position)
        {
            return position.x >= 0 && position.x < _configs.Row
                                   && position.y >= 0 && position.y < _configs.Column;
        }

        public void RemoveItem(string id, int count)
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

        public void RemoveItem(string id, Vector2Int position, int count)
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
            return GetMaxCellCount() - _configs.StartLockCellCount + _inventoryData.AdditionalCellCount;
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

        public void ClearCell(Vector2Int position)
        {
            _inventoryMap.Remove(position);
            CellChanged?.Invoke(position);
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