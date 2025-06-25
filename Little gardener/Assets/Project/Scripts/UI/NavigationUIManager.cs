using UnityEngine;
using LittleGardener.GameManagement;
using LittleGardener.Store;
using LittleGardener.Building;

namespace LittleGardener.UI
{
    [RequireComponent(typeof(InventoryUIManager))]
    [RequireComponent(typeof(WalletUIManager))]
    public class NavigationUIManager : MonoBehaviour
    {
        private BuildingManager _buildingManager;
        private InventoryUIManager _inventoryUIManager;

        [SerializeField] private StoreManager _storeManager;
        [SerializeField] private MainMenuManager _mainMenuManager;
        [SerializeField] private GameObject _showBuildStoreButton;
        [SerializeField] private GameObject _showStoreButton;
        [SerializeField] private GameObject _mainMenuButton;

        private void Awake()
        {
            _buildingManager = FindFirstObjectByType<BuildingManager>();
            _inventoryUIManager = GetComponent<InventoryUIManager>();
        }

        private void SetActivationButtonsState(bool state)
        {
            _showBuildStoreButton.SetActive(state);
            _showStoreButton.SetActive(state);
            _mainMenuButton.SetActive(state);
        }

        private void ShowMainInventory()
        {
            SetActivationButtonsState(false);
            _inventoryUIManager.ShowMainInventory();
        }

        private void HideMainInventory()
        {
            SetActivationButtonsState(true);
            _inventoryUIManager.HideMainInventory();
        }

        public void ShowBuildStore()
        {
            ControlsBlocker.DisablePlayerControls();
            SetActivationButtonsState(false);
            _inventoryUIManager.HideGeneralInventory();

            _buildingManager.Show();
        }

        public void HideBuildStore()
        {
            ControlsBlocker.EnablePlayerControls();
            SetActivationButtonsState(true);
            _inventoryUIManager.ShowGeneralInventory();

            _buildingManager.Hide();
        }

        public void ChangeMainInventoryState()
        {
            if (!_inventoryUIManager.IsMainInventoryActive)
            {
                ShowMainInventory();
                return;
            }

            HideMainInventory();
        }

        public void ShowStore()
        {
            _storeManager.Show();

            ControlsBlocker.DisablePlayerControls();
            SetActivationButtonsState(false);
            _inventoryUIManager.HideGeneralInventory();

        }

        public void HideStore()
        {
            _storeManager.Hide();

            ControlsBlocker.EnablePlayerControls();
            SetActivationButtonsState(true);
            _inventoryUIManager.ShowGeneralInventory();

        }

        public void ShowMainMenu()
        {
            _mainMenuManager.Show();

            ControlsBlocker.DisablePlayerControls();
            SetActivationButtonsState(false);
            _inventoryUIManager.HideGeneralInventory();
        }

        public void HideMainMenu()
        {
            _mainMenuManager.Hide();

            ControlsBlocker.EnablePlayerControls();
            SetActivationButtonsState(true);
            _inventoryUIManager.ShowGeneralInventory();
        }
    }
}
