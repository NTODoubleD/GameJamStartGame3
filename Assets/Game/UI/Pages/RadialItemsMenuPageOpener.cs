using DoubleDCore.UI.Base;
using UnityEngine.InputSystem;

namespace Game.UI.Pages
{
    public class RadialItemsMenuPageOpener
    {
        private readonly GameInput _gameInput;
        private readonly IUIManager _uiManager;
        
        private bool _isOpened;

        public RadialItemsMenuPageOpener(GameInput gameInput, IUIManager uiManager)
        {
            _gameInput = gameInput;
            _uiManager = uiManager;

            _gameInput.Player.InventoryOpen.performed += OnInventoryOpenRequested;
            _gameInput.UI.InventoryClose.performed += OnInventoryCloseRequested;
        }

        private void OnInventoryOpenRequested(InputAction.CallbackContext _)
        {
            if (_isOpened)
                return;
            
            _isOpened = true;
            _uiManager.OpenPage<RadialItemsMenuPage>();
            
            _gameInput.Player.Disable();
            _gameInput.UI.Enable();
        }
        
        private void OnInventoryCloseRequested(InputAction.CallbackContext _)
        {
            if (_isOpened == false)
                return;

            _isOpened = false;
            _uiManager.ClosePage<RadialItemsMenuPage>();
            
            _gameInput.Player.Enable();
            _gameInput.UI.Disable();
        }
        
        ~RadialItemsMenuPageOpener()
        {
            _gameInput.Player.InventoryOpen.performed -= OnInventoryOpenRequested;
            _gameInput.UI.InventoryClose.performed -= OnInventoryCloseRequested;
        }
    }
}