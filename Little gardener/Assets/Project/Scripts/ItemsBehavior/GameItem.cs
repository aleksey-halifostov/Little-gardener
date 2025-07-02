using LittleGardener.ItemsData;

namespace LittleGardener.ItemsBehaviour
{
    public class GameItem
    {
        public ItemData Data { get; }

        public GameItem(ItemData data)
        {
            Data = data;
        }
    }
}
