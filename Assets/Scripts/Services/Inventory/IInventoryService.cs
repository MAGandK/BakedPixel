using System;
using Services.Storage;
using UnityEngine;

namespace Services.Inventory
{
    public interface IInventoryService
    {
        event Action CellCountChanged;
        event Action<string> IdChanged;
        event Action<int> ItemCountChanged;
        event Action<string, int> ItemsAdded;
        event Action<string,int> ItemsRemoved;
        
        string Id { get; }
        int Cont { get; }
        bool IsEmpty { get; }
        
        void AddItem(string id);
        void AddItem(string id, Vector2Int position);
        void RemoveItem(string id);
        void RemoveItem(string id, Vector2Int position);

        int GetMaxCellCount();
        int GetUnlockCellCount();

        void UnlockCell();
    }
}