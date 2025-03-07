using System;
using System.Collections.Generic;

using UnityEngine;

namespace Services.Inventory
{
    public interface IInventoryService
    {
        event Action CellCountChanged;
        event Action<Vector2Int> CellChanged;

        Dictionary<Vector2Int, InventoryItemData> InventoryMap { get; }
        void AddItem(string id, int count);
        void AddItem(string id, Vector2Int position, int count);
        void RemoveItem(string id);
        void RemoveItem(string id, Vector2Int position);

        int GetMaxCellCount();
        int GetUnlockCellCount();

        void UnlockCell();
        InventoryItemData GetData(Vector2Int position);
    }
}