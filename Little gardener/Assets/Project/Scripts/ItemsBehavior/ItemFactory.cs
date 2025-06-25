using LittleGardener.ItemsData;

namespace LittleGardener.ItemsBehaviour
{
    public static class ItemFactory
    {
        public static GameItem CreateNewItem(ItemData data)
        {
            switch (data)
            {
                case SeedsData:
                    return new SeedItem(data as SeedsData);
                case PlantData:
                    return new PlantItem(data as PlantData);
                case ScissorsData:
                    return new ScissorsItem(data as ScissorsData);
                case WateringCanData:
                    return new WateringCanItem(data as WateringCanData);
                default:
                    return null;
            }
        }
    }
}