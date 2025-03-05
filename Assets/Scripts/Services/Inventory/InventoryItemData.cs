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
    }
}