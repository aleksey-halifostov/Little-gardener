namespace LittleGardener.ItemsBehaviour
{
    public class ItemDurabilityController
    {
        public int CurrentDurability {  get; private set; }

        public ItemDurabilityController(int currentDurability)
        {
            CurrentDurability = currentDurability;
        }

        public void DecreaseDurability()
        {
            CurrentDurability--;
        }
    }
}