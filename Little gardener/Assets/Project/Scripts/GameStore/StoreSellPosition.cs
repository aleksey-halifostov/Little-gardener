using LittleGardener.ItemsBehaviour;

namespace LittleGardener.GameStore
{
    public class StoreSellPosition : DetailsPanelPosition
    {
        private int _maximumAmount;

        public void Init(GameItem item, int price, Store storeManager, int maximumAmount)
        {
            base.Init(item, price, storeManager);

            _maximumAmount = maximumAmount;
        }

        public void SellItem()
        {
            _storeManager.SellInventoryItem(_item, GetAmount(), _price);
            SetDefaultAmount();
        }

        public override void AmountIncrement()
        {
            if (GetAmount() < _maximumAmount)
            {
                base.AmountIncrement();
            }
        }
    }
}
