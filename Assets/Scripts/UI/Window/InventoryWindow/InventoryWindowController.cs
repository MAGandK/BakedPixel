using System.Linq;
using Services.Inventory;
using UnityEngine;

namespace UI.Window.InventoryWindow
{
    public class InventoryWindowController : AbstractWindowController<InventoryWindowView>
    {
        private readonly IInventoryService _inventoryService;
        private readonly InventoryWindowView _view;
        private readonly InventoryConfig _inventoryConfig;

        public InventoryWindowController(
            InventoryWindowView view,
            IInventoryService inventoryService,
            InventoryConfig inventoryConfig
        ) : base(view)
        {
            _inventoryService = inventoryService;
            _view = view;
            _inventoryConfig = inventoryConfig;
        }

        public override void Initialize()
        {
            base.Initialize();

            var rows = _inventoryConfig.Row;
            var columns = _inventoryConfig.Column;
            var unlockCells = _inventoryService.GetUnlockCellCount();

            _view.PrepareCells(rows, columns, unlockCells);
            _view.FillCells(_inventoryService.InventoryMap, GetSprite);

            _view.SubscribeButton(OnAddRandomItemButtonClick, OnDeleteButtonClick);
        }

        private Sprite GetSprite(string id)
        {
            return _inventoryConfig.ItemConfigs.FirstOrDefault(x => x.Id.Equals(id))?.Icon;
        }

        protected override void OnShow()
        {
            base.OnShow();

            _inventoryService.CellChanged += InventoryServiceOnCellChanged;
        }

        protected override void OnHide()
        {
            base.OnHide();
            _inventoryService.CellChanged -= InventoryServiceOnCellChanged;
        }

        private InventoryItemConfig GetRandomConfig()
        {
            var randomItemIndex = Random.Range(0, _inventoryConfig.ItemConfigs.Length);
            return _inventoryConfig.ItemConfigs[randomItemIndex];
        }

        private void OnAddRandomItemButtonClick()
        {
            var inventoryItemConfig = GetRandomConfig();
            var range = Random.Range(0, inventoryItemConfig.MaxCount);
            Debug.Log($"Adding item: {inventoryItemConfig.Id}, count: {range}");
            _inventoryService.AddItem(inventoryItemConfig.Id, range);
        }

        private void OnDeleteButtonClick()
        {
            Debug.Log("Delete button clicked");
            var inventoryItemConfig = GetRandomConfig();

            _inventoryService.RemoveItem(inventoryItemConfig.Id, 1);
        }

        private void InventoryServiceOnCellChanged(Vector2Int position)
        {
            var data = _inventoryService.GetData(position);

            if (data == null)
            {
                _view.RefreshCell(position, 0, null);
                return;
            }

            _view.RefreshCell(position, data.Count, GetSprite(data.ID));
        }
    }
}