using UnityEngine;

namespace Services.Inventory
{
    [CreateAssetMenu(menuName = "Create InventoryConfig", fileName = "InventoryConfig", order = 0)]
    public class InventoryConfig : ScriptableObject
    {
        [SerializeField] private int _column;
        [SerializeField] private int _row;
        [SerializeField] private int _startLockCellCount;
        [SerializeField] private InventoryItemConfig[] _itemConfigs;
        
        public int Column => _column;
        public int Row => _row;
        public int StartLockCellCount => _startLockCellCount;

        public InventoryItemConfig[] ItemConfigs => _itemConfigs;
    }
}