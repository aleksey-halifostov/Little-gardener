using System.Collections.Generic;
using LittleGardener.Garden;
using LittleGardener.Inventory;
using LittleGardener.ItemsData;
using LittleGardener.GameManagement;

namespace LittleGardener.ItemsBehaviour
{
    public class ScissorsItem : ItemWithDurability, IUsable
    {
        private ScissorsData _data;

        public ScissorsItem(ScissorsData data) : base(data) 
        { 
            _data = data;
        }

        public void Use(GardenBed bed, IItemContainer container, IItemDemonstrationUpdator slot, AudioManager audioManager)
        {
            if (bed.IsReadyToCollect())
            {
                PlantItem plant = bed.CheckPlant();

                if (container.TryGetFreeSlotsFor(plant, 1, out List<InventorySlot> slots))
                {
                    container.AddItem(plant, 1, slots);
                    bed.RemovePlant();
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
    }
}