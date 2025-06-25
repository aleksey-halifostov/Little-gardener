using UnityEngine;
using UnityEngine.UI;
using System;
using LittleGardener.PlayerControls;
using LittleGardener.ItemsBehaviour;
using LittleGardener.ItemsData;
using LittleGardener.GameManagement;

namespace LittleGardener.Inventory
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(InventorySlot))]
    public class QuickSlot : MonoBehaviour, IItemDemonstrationUpdator
    {
        private Image _image;
        private InventorySlot _slot;
        private PlayerController _playerController;
        private Color _activeColor = new Color(.65f, .5f, .3f, 1.0f);
        private Color _simpleColor = new Color(1f, 1f, 1f, 1f);
        private bool _isActive = false;

        public static event Action OnSlotInteracted;
        public static event Action<string> OnSlotActivated;

        public bool IsActive { get { return _isActive; } private set { } }

        private void Awake()
        {
            _image = GetComponent<Image>();
            _slot = GetComponent<InventorySlot>();
            _playerController = FindFirstObjectByType<PlayerController>();
        }

        private void SetAsActive()
        {
            _image.color = _activeColor;
            OnSlotActivated?.Invoke(_slot.GetItem().Data.ItemName);
        }

        private void SetPlayerHandObject()
        {
            GameItem item = _slot.GetItem();

            if (item == null) { return; }

            if (item.Data.IsHandle)
            {
                _playerController.SetHand(this);
                SetAsActive();
                _isActive = true;
            }

        }
        private void SetAsNotActive()
        {
            _image.color = _simpleColor;
        }

        public void Deactivate()
        {
            _playerController.ResetHand();
            SetAsNotActive();
            _isActive = false;
        }

        public void ActivateSlot()
        {
            if (_isActive)
            {
                Deactivate();
                return;
            }

            OnSlotInteracted?.Invoke();
            SetPlayerHandObject();
        }

        public GameItem GetItem()
        {
            return _slot.GetItem();
        }

        public void RemoveItem(ItemData data, int amount)
        {
            if (data != _slot.GetItem().Data)
                throw new System.ArgumentException(nameof(data));

            if (amount > _slot.GetItemAmount())
                throw new System.ArgumentOutOfRangeException(nameof(amount));

            _slot.RemoveItem(amount);

            if (_slot.IsFree)
            {
                Deactivate();
                StartCoroutine(ControlsBlocker.EnableCameraMover());
            }
        }

        public void UpdateDurability(int max, int current)
        {
            _slot.ShowCurrentDurability(max, current);
        }
    }
}
