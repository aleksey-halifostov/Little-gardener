using LittleGardener.ItemsData;

namespace LittleGardener.ItemsBehaviour 
{ 
    public class ItemWithDurability : GameItem
    {
        protected ItemDurabilityController _durabilityController;

        public int CurrentDurability => _durabilityController.CurrentDurability;

        public ItemWithDurability(DurabilityItemData data) : base(data)
        {
            _durabilityController = new ItemDurabilityController(data.MaxDurability);
        }
    }
}