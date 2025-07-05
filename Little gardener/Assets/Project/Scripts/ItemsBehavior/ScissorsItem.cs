using System.Collections.Generic;
using LittleGardener.Garden;
using LittleGardener.GameInventory;
using LittleGardener.ItemsData;
using LittleGardener.GameManagement;

namespace LittleGardener.ItemsBehaviour
{
    public class ScissorsItem : ToolItem, IUsable
    {
        private AudioManager _audioManager;
        private IItemContainer _container;

        public ScissorsItem(ToolData data, AudioManager audioManager, IItemContainer container) : base(data) 
        {
            if (data.ToolType != ToolType.Scissors)
                throw new System.ArgumentException($"Watering Can needs ToolData with ToolType.Scissors, got {data.ToolType}");
            if (audioManager == null)
                throw new System.ArgumentNullException(nameof(audioManager));
            if (container == null)
                throw new System.ArgumentNullException(nameof(container));

            _audioManager = audioManager;
            _container = container;
        }

        public bool Use(IInteractable interactable)
        {
            if (interactable is GardenBed bed && bed.IsReadyToCollect())
            {
                PlantItem plant = bed.CheckPlant();

                if (_container.TryGetFreeSlotsFor(plant, 1, out List<InventorySlot> slots))
                {
                    _container.AddItem(plant, 1, slots);
                    bed.RemovePlant();
                    UpdateDurability(_audioManager);
                }
                else
                    _container.ShowMessage();
            }

            return IsReadyToDestroy();
        }
    }
}