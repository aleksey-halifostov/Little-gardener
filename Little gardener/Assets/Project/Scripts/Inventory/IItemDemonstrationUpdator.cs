using LittleGardener.ItemsData;

namespace LittleGardener.Inventory
{
    public interface IItemDemonstrationUpdator
    {
        public void UpdateDurability(int max, int current);
        public void RemoveItem(ItemData data, int amount);
    }
}