using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Services.Inventory
{
    [Serializable]
    public class InventoryItemData
    {
        [JsonProperty("id")] private string _id;
        [JsonProperty("count")] private int _count;
        [JsonProperty("position")] private Vector2Int _position;

        public Vector2Int Position => _position;
        public string ID => _id;
        public int Count => _count;

        public InventoryItemData(string id, int count, Vector2Int position)
        {
            _id = id;
            _count = count;
            _position = position;
        }

        public void AddCount(int count)
        {
            _count = count;
        }
    }
}