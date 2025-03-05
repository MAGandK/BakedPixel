using UnityEngine;

namespace Services.Inventory
{
    [CreateAssetMenu(menuName = "Create InventoryConfig", fileName = "InventoryConfig", order = 0)]
    public class InventoryConfig : ScriptableObject
    {
        [SerializeField] private int _maxCellLine;//максимальное количество рядов в инвентаре.
        [SerializeField] private int _cellLineItemCount;//количество ячеек в каждом ряду.
        [SerializeField] private int _startLockCellCount;
        [SerializeField] private InventoryItemConfig[] _itemConfigs;
        
        public int MaxCellLine => _maxCellLine;
        public int CellLineItemCount => _cellLineItemCount;
        public int StartLockCellCount => _startLockCellCount;

        public InventoryItemConfig[] ItemConfigs => _itemConfigs;
    }
}