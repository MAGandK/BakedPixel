namespace Services.Storage
{
    public struct RemoveItemsFromInventoryResult
    {
        public readonly string InventoryId;
        public readonly int ItemsToAddCount;
        public readonly bool Success;
        

        public RemoveItemsFromInventoryResult(string inventoryId, int itemsToAddCount, bool success)
        {
            InventoryId = inventoryId;
            ItemsToAddCount = itemsToAddCount;
            Success = success;
        }
    }
}