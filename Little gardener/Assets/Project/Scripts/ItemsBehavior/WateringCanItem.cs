using UnityEngine;
using LittleGardener.GameManagement;
using LittleGardener.Garden;
using LittleGardener.ItemsData;

namespace LittleGardener.ItemsBehaviour
{
    public class WateringCanItem : ToolItem, IUsable
    {
        AudioManager _audioManager;

        public WateringCanItem(ToolData data, AudioManager audioManager) : base(data) 
        {
            if (data.ToolType != ToolType.WateringCan)
                throw new System.ArgumentException($"Watering Can needs ToolData with ToolType.WateringCan, got {data.ToolType}");
            if (audioManager == null)
                throw new System.ArgumentNullException(nameof(audioManager));

            _audioManager = audioManager;
        }

        public bool Use(IInteractable interactable)
        {
            if (interactable is GardenBed bed)
            {
                bed.WaterBed();
                UpdateDurability(_audioManager);
            }

            return IsReadyToDestroy();
        }
    }
}