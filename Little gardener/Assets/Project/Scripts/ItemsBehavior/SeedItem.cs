using LittleGardener.GameManagement;
using LittleGardener.Garden;
using LittleGardener.Inventory;
using LittleGardener.ItemsData;

namespace LittleGardener.ItemsBehaviour
{
    public class SeedItem : GameItem, IUsable
    {
        private SeedsData _data;

        public SeedItem(SeedsData data) : base(data) 
        {
            this._data = data;
        }

        public void Use(GardenBed bed, IItemContainer container, IItemDemonstrationUpdator slot, AudioManager audioManager)
        {
            if (bed.IsFree)
            {
                bed.SetPlant((PlantItem)ItemFactory.CreateNewItem(_data.Plant));
                slot.RemoveItem(_data, 1);
            }
        }
    }
}