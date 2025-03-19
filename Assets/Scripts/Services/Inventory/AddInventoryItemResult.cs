namespace Services.Inventory
{
    public struct AddInventoryItemResult
    {
        public bool IsAdded { get; }
        public int RemainCount { get; }
        
        public AddInventoryItemResult(bool isAdded, int remainCount)
        {
            IsAdded = isAdded;
            RemainCount = remainCount;
        }
    }
}