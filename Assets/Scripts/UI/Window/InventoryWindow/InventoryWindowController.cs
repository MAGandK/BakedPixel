using Services.Inventory;

namespace UI.Window.InventoryWindow
{
    public class InventoryWindowController : AbstractWindowController<InventoryWindowView>
    {
        private IInventoryService _inventoryService;
        private readonly InventoryWindowView _view;
        private readonly InventoryConfig _inventoryConfig;

        public InventoryWindowController(InventoryWindowView view, IInventoryService inventoryService,
            InventoryConfig inventoryConfig) : base(view)
        {
            _inventoryService = inventoryService;
            _view = view;
            _inventoryConfig = inventoryConfig;
        }

        public override void Initialize()
        {
            base.Initialize();
            
            int rows = _inventoryConfig.MaxCellLine; 
            int columns = _inventoryConfig.CellLineItemCount; 
            int unlockCells = _inventoryService.GetUnlockCellCount(); 

            _view.PrepareCells(rows, columns, unlockCells);
            
            _view.SubscribeButton(OnAddItemButtonClick, OnDeleteButtonClick);
        }
        
        private void OnDeleteButtonClick()
        {
           _inventoryService.RemoveItem("");
     
        }

        private void OnAddItemButtonClick()
        {
            _inventoryService.AddItem("");
         
        }
    }
    
}