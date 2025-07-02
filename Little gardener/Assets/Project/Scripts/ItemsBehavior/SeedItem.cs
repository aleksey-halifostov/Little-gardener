using LittleGardener.GameManagement;
using LittleGardener.Garden;
using LittleGardener.ItemsData;

namespace LittleGardener.ItemsBehaviour
{
    public class SeedItem : GameItem, IUsable
    {
        private SeedsData _data;

        public SeedItem(SeedsData data) : base(data) 
        {
            _data = data;
        }

        public bool Use(IInteractable interactable)
        {
            if (interactable is GardenBed bed && bed.IsFree)
            {
                bed.SetPlant((PlantItem)ItemFactory.CreateNewItem(_data.Plant));
                return true;
            }

            return false;
        }
    }
}