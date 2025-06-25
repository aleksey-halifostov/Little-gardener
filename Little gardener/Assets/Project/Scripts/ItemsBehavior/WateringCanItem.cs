using LittleGardener.GameManagement;
using LittleGardener.Garden;
using LittleGardener.Inventory;
using LittleGardener.ItemsData;

namespace LittleGardener.ItemsBehaviour
{
    public class WateringCanItem : ItemWithDurability, IUsable
    {
        private WateringCanData _data;

        public WateringCanItem(WateringCanData data) : base(data) 
        { 
            _data = data;
        }

        public void Use(GardenBed bed, IItemContainer container, IItemDemonstrationUpdator slot, AudioManager audioManager)
        {
            bed.WaterBed();
            _durabilityController.DecreaseDurability();

            if (_durabilityController.CurrentDurability == 0)
            {
                slot.RemoveItem(_data, 1);
                audioManager.PlayEffectSound(_data.DestroySound);
            }
            else
            {
                audioManager.PlayEffectSound(_data.ToolSound);
                slot.UpdateDurability(_data.MaxDurability, _durabilityController.CurrentDurability);
            }
        }
    }
}