using UnityEngine;
using LittleGardener.Garden;
using LittleGardener.Inventory;
using LittleGardener.ItemsBehaviour;
using LittleGardener.GameManagement;

namespace LittleGardener.PlayerControls
{
    public class PlayerController : MonoBehaviour
    {
        private InventoryManager _inventoryManager;
        private QuickSlot _handSlot;
        private AudioManager _audioManager;

        private void Awake()
        {
            _inventoryManager = FindFirstObjectByType<InventoryManager>();
            _audioManager = FindFirstObjectByType<AudioManager>();
        }

        private void Update()
        {
            if (_handSlot != null && InputManager.TryGetTouchCollider(out Collider2D collider))
            {
                TryToInteract(collider);
            }
        }

        private void TryToInteract(Collider2D collider)
        {
            if (collider.TryGetComponent<GardenBed>(out GardenBed bed))
            {
                if (_handSlot != null)
                {
                    GameItem item = _handSlot.GetItem();

                    if (item is IUsable)
                    {
                        IUsable tool = (IUsable)_handSlot.GetItem();

                        tool.Use(bed, _inventoryManager, _handSlot, _audioManager);
                    }
                }
            }

        }

        public void SetHand(QuickSlot activeSlot)
        {
            _handSlot = activeSlot;
        }

        public void ResetHand()
        {
            _handSlot = null;
        }
    }
}
