using UnityEngine;
using System;
using LittleGardener.PlayerControls;
using LittleGardener.ItemsBehaviour;
using LittleGardener.ItemsData;

namespace LittleGardener.GameInventory
{
    [RequireComponent(typeof(InventorySlot))]
    [RequireComponent(typeof(QuickSlotView))]
    public class QuickSlot : MonoBehaviour, IItemUser

    {
        private InventorySlot _slot;
        private bool _isActive = false;
        private QuickSlotView _view;
        private PlayerController _playerController;

        public static event Action OnSlotInteracted;
        public static event Action<string> OnSlotActivated;

        public bool IsActive => _isActive;

        private void Awake()
        {
            _view = GetComponent<QuickSlotView>();
            _slot = GetComponent<InventorySlot>();

            _playerController = FindFirstObjectByType<PlayerController>();

            if (_playerController == null)
                throw new NullReferenceException(nameof(_playerController));
        }

        private void SetPlayerHandObject()
        {
            GameItem item = _slot.GetItem();

            if (item == null) { return; }

            if (item is IUsable)
            {
                _playerController.SetHand(this);
                _view.SetActiveColor();
                OnSlotActivated?.Invoke(_slot.GetItem().Data.ItemName);
                _isActive = true;
            }

        }

        public void Deactivate()
        {
            _playerController.ResetHand();
            _view.SetInactiveColor();
            _isActive = false;
        }

        public void TryChangeSlotState()
        {
            if (_isActive)
            {
                Deactivate();
                return;
            }

            OnSlotInteracted?.Invoke();
            SetPlayerHandObject();
        }

        public void UseItem(IInteractable interactable)
        {
            IUsable usable = _slot.GetItem() as IUsable;
            bool isReadyToDestroy = usable.Use(interactable);

            if (isReadyToDestroy)
            {
                _slot.RemoveItem(1);
                
                if (_slot.IsFree)
                    Deactivate();
            }
            else if (usable is ToolItem tool)
            {
                ToolData toolData = tool.Data as ToolData;
                _slot.SetCurrentDurability(toolData.MaxDurability, tool.CurrentDurability);
            }
        }
    }
}
