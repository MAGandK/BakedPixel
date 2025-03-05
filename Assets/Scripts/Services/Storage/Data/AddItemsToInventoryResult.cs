namespace Services.Storage
{
    public struct AddItemsToInventoryResult
    {
        public readonly string InventoryId;
        public readonly int ItemsToAddCount;
        public readonly int ItemsAddedCount;

        public int ItemsNoAddedCount => ItemsToAddCount - ItemsAddedCount;

        public AddItemsToInventoryResult(string inventoryId, int itemsToAddCount, int itemsAddedCount)
        {
            InventoryId = inventoryId;
            ItemsToAddCount = itemsToAddCount;
            ItemsAddedCount = itemsAddedCount;
        }
    }
}