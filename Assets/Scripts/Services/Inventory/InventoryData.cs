using System.Collections.Generic;
using Newtonsoft.Json;
using Services.Storage;

namespace Services.Inventory
{
    [JsonObject(MemberSerialization.OptIn)]
    public class InventoryData : AbstractStorageData<InventoryData>
    {
        [JsonProperty("inventoryItems")] private List<InventoryItemData> _inventoryItems = new List<InventoryItemData>();
        [JsonProperty] private int _additionalCellCount;

        public int AdditionalCellCount => _additionalCellCount;
        public List<InventoryItemData> InventoryItems => _inventoryItems;
        

        public InventoryData(string key) : base(key)
        {
        }

        public override void Load(InventoryData data)
        {
            _inventoryItems = data._inventoryItems;
            _additionalCellCount = data._additionalCellCount;
        }

        public void UnlockCell()
        {
            _additionalCellCount++;
            OnChanged();
        }

        public void SetNewInventoryItems(List<InventoryItemData> inventoryItemDatas)
        {
            _inventoryItems = inventoryItemDatas;
            OnChanged();
        }
    }
}