using LittleGardener.ItemsBehaviour;
using System.Collections.Generic;

namespace LittleGardener.Inventory
{
    public interface IItemStorage
    {
        public Dictionary<GameItem, int> GetItemsForSale();
        public bool TryGetSlotsWith(GameItem item, int amount, out List<InventorySlot> foundSlots);
        public void RemoveItemByType(GameItem item, int amount, List<InventorySlot> slots);
    }
}