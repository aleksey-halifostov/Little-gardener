using System;
using LittleGardener.ItemsData;
using LittleGardener.ItemsBehaviour;
using LittleGardener.GameInventory;

namespace LittleGardener.GameManagement
{
    public static class ItemFactory
    {
        private static AudioManager _audioManager;
        private static IItemContainer _itemContainer;

        private static GameItem CreateTool(ToolData toolData)
        {
            switch (toolData.ToolType)
            {
                case ToolType.WateringCan:
                    return new WateringCanItem(toolData, _audioManager);
                case ToolType.Scissors:
                    return new ScissorsItem(toolData, _audioManager, _itemContainer);
                default:
                    throw new ArgumentException($"Unsupported Tool Type: {toolData.ToolType}");
            }
        }

        public static void Init(AudioManager audioManager, IItemContainer container)
        {
            if (audioManager == null)
                throw new ArgumentNullException(nameof(audioManager));
            if (container == null)
                throw new ArgumentNullException(nameof(container));

            _audioManager = audioManager;
            _itemContainer = container;
        }

        public static GameItem CreateNewItem(ItemData data)
        {
            switch (data.ItemType)
            {
                case ItemType.Seed:
                    return new SeedItem(data as SeedsData);
                case ItemType.Plant:
                    return new PlantItem(data as PlantData);
                case ItemType.Tool:
                    return CreateTool(data as ToolData);
                default:
                    throw new ArgumentException($"Unsupported Item Type: {data.ItemType}");
            }
        }
    }
}