using LittleGardener.GameManagement;
using LittleGardener.Garden;
using LittleGardener.Inventory;

namespace LittleGardener.ItemsBehaviour
{
    public interface IUsable
    {
        public void Use(GardenBed bed, IItemContainer container, IItemDemonstrationUpdator slot, AudioManager audioManager);
    }
}