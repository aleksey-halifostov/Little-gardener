using LittleGardener.ItemsBehaviour;
using System.Collections.Generic;

namespace LittleGardener.Inventory
{
    public interface IItemContainer
    {
        public void AddItem(GameItem item, int amount, List<InventorySlot> slots);
        public bool TryGetFreeSlotsFor(GameItem item, int amount, out List<InventorySlot> foundSlots);
    }
}