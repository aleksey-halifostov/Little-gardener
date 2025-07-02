using UnityEngine;
using System.Collections.Generic;
using LittleGardener.ItemsBehaviour;
using LittleGardener.GameManagement;
using LittleGardener.UI;

namespace LittleGardener.GameInventory
{
    public class Inventory : MonoBehaviour, IItemContainer
    {
        private List<InventorySlot> _slots = new List<InventorySlot>();
        
        [SerializeField] private InventoryUI _inventoryUI;

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

            if (_slots.Count == 0)
                throw new System.InvalidOperationException("Slots list can not be empty at this point");
        }

        public void AddItem(GameItem item, int amount, List<InventorySlot> slots)
        {
            if (amount < 1)
                throw new System.ArgumentOutOfRangeException(nameof(amount));
            if (item == null)
                throw new System.ArgumentNullException(nameof(item));

            foreach (InventorySlot slot in slots)
            {
                if (!slot.IsFree && slot.GetItem().Data.ItemName != item.Data.ItemName)
                    throw new System.ArgumentException($"Inventory: Need slot with {item.Data.ItemName}, " +
                        $"got filled or slot with other item type. Got slot item: {slot.GetItem().Data.ItemName}.", nameof(slots));

                int freePlaces = item.Data.MaximumAmount - slot.GetItemAmount();

                if (amount - freePlaces <= 0)
                {
                    slot.AddItem(item, amount);
                    return;
                }
                else
                {
                    slot.AddItem(item, freePlaces);
                    amount -= freePlaces;
                    item = ItemFactory.CreateNewItem(item.Data);
                }
            }

            if (amount > 0)
                throw new System.ArgumentOutOfRangeException(nameof(amount));
        }

        public bool TryGetFreeSlotsFor(GameItem item, int amount, out List<InventorySlot> foundSlots)
        {
            if (amount < 1)
                throw new System.ArgumentOutOfRangeException(nameof(amount));
            if (item == null)
                throw new System.ArgumentNullException(nameof(item));

            foundSlots = new List<InventorySlot>();

            foreach (InventorySlot slot in _slots)
            {
                if (!slot.IsFree && slot.GetItem().Data.ItemName == item.Data.ItemName && slot.GetItemAmount() < item.Data.MaximumAmount)
                {
                    foundSlots.Add(slot);
                    amount -= item.Data.MaximumAmount - slot.GetItemAmount();

                    if (amount <= 0)
                        return true;
                }
            }

            foreach (InventorySlot slot in _slots)
            {
                if (slot.IsFree)
                {
                    foundSlots.Add(slot);
                    amount -= item.Data.MaximumAmount;

                    if (amount <= 0)
                        return true;
                }
            }

            foundSlots = null;
            return false;
        }

        public void ShowMessage()
        {
            _inventoryUI.ShowLittleSpaceMessage();
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
            if (amount < 1)
                throw new System.ArgumentOutOfRangeException(nameof(amount));
            if (item == null)
                throw new System.ArgumentNullException(nameof(item));

            foundSlots = new List<InventorySlot>();

            foreach (InventorySlot slot in _slots)
            {
                if (!slot.IsFree && slot.GetItem().Data.ItemName == item.Data.ItemName)
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
            if (amount < 1)
                throw new System.ArgumentOutOfRangeException(nameof(amount));
            if (item == null)
                throw new System.ArgumentNullException(nameof(item));

            foreach (InventorySlot slot in slots)
            {
                if (slot.IsFree || slot.GetItem().Data.ItemName != item.Data.ItemName)
                    throw new System.ArgumentException($"Inventory: need slots with {item.Data.ItemName}, got free or" +
                        $" slot with other item. Got slot with {slot.GetItem().Data.ItemName}", nameof(slots));

                int amountDelta = amount - slot.GetItemAmount();

                if (amountDelta <= 0)
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
                throw new System.ArgumentException(nameof(slots));
        }
    }
}
