using UI;
using UI.Window.InventoryWindow;
using UnityEngine;
using Zenject;

public class Test : MonoBehaviour
{
    private IUIController _uiController;
    
    [Inject]
    private void Construct(IUIController uiController)
    {
        _uiController = uiController;
    }

    private void Awake()
    {
        _uiController.ShowWindow<InventoryWindowController>();
    }
}
