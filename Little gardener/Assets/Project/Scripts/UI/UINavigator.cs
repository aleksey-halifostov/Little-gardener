using UnityEngine;
using LittleGardener.GameManagement;
using LittleGardener.GameStore;
using LittleGardener.Building;
using LittleGardener.GameInventory;

namespace LittleGardener.UI
{
    public class UINavigator : MonoBehaviour
    {
        [SerializeField] private BuildingManager _buildingManager;
        [SerializeField] private InventoryUI _inventoryUI;
        [SerializeField] private Store _storeManager;
        [SerializeField] private MainMenuNavigator _mainMenu;
        [SerializeField] private GameObject _showBuildStoreButton;
        [SerializeField] private GameObject _showStoreButton;
        [SerializeField] private GameObject _mainMenuButton;

        private void SetActivationButtonsState(bool state)
        {
            _showBuildStoreButton.SetActive(state);
            _showStoreButton.SetActive(state);
            _mainMenuButton.SetActive(state);
        }

        private void ShowMainInventory()
        {
            SetActivationButtonsState(false);
            _inventoryUI.ShowMainInventory();
        }

        private void HideMainInventory()
        {
            SetActivationButtonsState(true);
            _inventoryUI.HideMainInventory();
        }

        public void ShowBuildStore()
        {
            ControlsBlocker.DisableControls();
            SetActivationButtonsState(false);
            _inventoryUI.HideGeneralInventory();

            _buildingManager.Show();
        }

        public void HideBuildStore()
        {
            ControlsBlocker.EnableControls();
            SetActivationButtonsState(true);
            _inventoryUI.ShowGeneralInventory();

            _buildingManager.Hide();
        }

        public void ChangeMainInventoryState()
        {
            if (!_inventoryUI.IsMainInventoryActive)
            {
                ShowMainInventory();
                return;
            }

            HideMainInventory();
        }

        public void ShowStore()
        {
            _storeManager.Show();

            ControlsBlocker.DisableControls();
            SetActivationButtonsState(false);
            _inventoryUI.HideGeneralInventory();

        }

        public void HideStore()
        {
            _storeManager.Hide();

            ControlsBlocker.EnableControls();
            SetActivationButtonsState(true);
            _inventoryUI.ShowGeneralInventory();

        }

        public void ShowMainMenu()
        {
            _mainMenu.Show();

            ControlsBlocker.DisableControls();
            SetActivationButtonsState(false);
            _inventoryUI.HideGeneralInventory();
        }

        public void HideMainMenu()
        {
            _mainMenu.Hide();

            ControlsBlocker.EnableControls();
            SetActivationButtonsState(true);
            _inventoryUI.ShowGeneralInventory();
        }
    }
}
