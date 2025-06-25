using UnityEngine;
using System.Collections.Generic;
using LittleGardener.UI;
using LittleGardener.ItemsBehaviour;

namespace LittleGardener.Inventory
{
    public class InventoryManager : MonoBehaviour, IItemStorage, IItemContainer
    {
        private InventoryUIManager _inventoryUIManager;
        private List<InventorySlot> _slots = new List<InventorySlot>();

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i).GetChild(0);
                for (int j = 0; j < child.childCount; j++)
                {
                    if (child.GetChild(j).gameObject.TryGetComponent<InventorySlot>(out InventorySlot slot))
                    {
                        _slots.Add(slot);
                    }
                }
            }

            _inventoryUIManager = FindFirstObjectByType<InventoryUIManager>();
        }

        public void AddItem(GameItem item, int amount, List<InventorySlot> slots)
        {
            foreach (InventorySlot slot in slots)
            {
                int freePlaces = item.Data.MaximumAmount - slot.GetItemAmount();

                if (amount - freePlaces <= 0)
                {
                    slot.AddItem(item, amount);
                    break;
                }
                else
                {
                    slot.AddItem(item, freePlaces);
                    amount -= freePlaces;
                    item = ItemFactory.CreateNewItem(item.Data);
                }
            }
        }

        public bool TryGetFreeSlotsFor(GameItem item, int amount, out List<InventorySlot> foundSlots)
        {
            foundSlots = new List<InventorySlot>();


            foreach (InventorySlot slot in _slots)
            {
                if (!slot.IsFree && slot.GetItem().Data == item.Data && slot.GetItemAmount() < item.Data.MaximumAmount)
                {
                    foundSlots.Add(slot);
                    amount -= item.Data.MaximumAmount - slot.GetItemAmount();

                    if (amount <= 0)
                    {
                        return true;
                    }
                }
            }

            foreach (InventorySlot slot in _slots)
            {
                if (slot.IsFree)
                {
                    foundSlots.Add(slot);
                    amount -= item.Data.MaximumAmount;

                    if (amount <= 0)
                    {
                        return true;
                    }
                }
            }

            foundSlots = null;
            return false;
        }

        public void ShowMessage()
        {
            _inventoryUIManager.ShowLittleSpaceMessage();
        }

        public Dictionary<GameItem, int> GetItemsForSale()
        {
            Dictionary<GameItem, int> inventoryItems = new Dictionary<GameItem, int>();

            foreach (InventorySlot slot in _slots)
            {
                if (!slot.IsFree)
                {
                    if (inventoryItems.ContainsKey(slot.GetItem()))
                    {
                        inventoryItems[slot.GetItem()] += slot.GetItemAmount();
                    }
                    else
                    {
                        inventoryItems.Add(slot.GetItem(), slot.GetItemAmount());
                    }
                }
            }

            return inventoryItems;
        }

        public bool TryGetSlotsWith(GameItem item, int amount, out List<InventorySlot> foundSlots)
        {
            foundSlots = new List<InventorySlot>();

            foreach (InventorySlot slot in _slots)
            {
                if (!slot.IsFree && slot.GetItem().Data == item.Data)
                {
                    amount -= slot.GetItemAmount();
                    foundSlots.Add(slot);

                    if (amount <= 0)
                    {
                        return true;
                    }
                }
            }

            foundSlots = null;
            return false;
        }

        public void RemoveItemByType(GameItem item, int amount, List<InventorySlot> slots)
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.IsFree || slot.GetItem().Data != item.Data)
                    throw new System.ArgumentException(nameof(slots));

                int amountDelta = amount - slot.GetItemAmount();

                if (amountDelta < 0)
                {
                    slot.RemoveItem(amount);
                    return;
                }
                else
                {
                    slot.ClearSlot();
                    amount = amountDelta;
                }
            }

            if (amount > 0)
                throw new System.ArgumentException(nameof(amount));
        }
    }
}
