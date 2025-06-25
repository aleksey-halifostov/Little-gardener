using UnityEngine;
using System.Collections.Generic;
using LittleGardener.UI;

namespace LittleGardener.Inventory
{
    public class QuickSlotManager : MonoBehaviour
    {
        private List<QuickSlot> _quickSlots = new List<QuickSlot>();
        private InventoryUIManager _inventoryUIManager;

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent<QuickSlot>(out QuickSlot slot))
                {
                    _quickSlots.Add(slot);
                }
            }

            _inventoryUIManager = FindFirstObjectByType<InventoryUIManager>();
        }

        private void OnEnable()
        {
            QuickSlot.OnSlotInteracted += ResetSlots;
            QuickSlot.OnSlotActivated += ShowItemName;
        }

        private void OnDisable()
        {
            QuickSlot.OnSlotInteracted -= ResetSlots;
            QuickSlot.OnSlotActivated -= ShowItemName;
        }

        private void ResetSlots()
        {
            foreach (QuickSlot slot in _quickSlots)
            {
                slot.Deactivate();
            }
        }

        private void ShowItemName(string name)
        {
            _inventoryUIManager.ShowItemName(name);
        }
    }
}
