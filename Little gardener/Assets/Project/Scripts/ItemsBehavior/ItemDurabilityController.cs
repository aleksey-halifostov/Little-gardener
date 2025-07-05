namespace LittleGardener.ItemsBehaviour
{
    public class ItemDurabilityController
    {
        public int CurrentDurability {  get; private set; }

        public ItemDurabilityController(int currentDurability)
        {
            if (currentDurability <= 0)
                throw new System.ArgumentOutOfRangeException(nameof(currentDurability));

            CurrentDurability = currentDurability;
        }

        public void DecreaseDurability()
        {
            if (CurrentDurability == 0)
                throw new System.InvalidOperationException("Durability can not be less then 0");

            CurrentDurability--;
        }
    }
}