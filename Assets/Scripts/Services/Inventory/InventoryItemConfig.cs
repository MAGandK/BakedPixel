using UnityEngine;

namespace Services.Inventory
{
    [CreateAssetMenu(menuName = "Create InventoryItemConfig", fileName = "InventoryItemConfig", order = 0)]
    public class InventoryItemConfig : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private Sprite _icon;
        [SerializeField] private float _weight;
        [SerializeField] private int _maxCount;

        public string Id => _id;
        public Sprite Icon => _icon;
        public float Weight => _weight;
        public int MaxCount => _maxCount;
    }
}