namespace LittleGardener.GameStore
{
    public class StoreBuyPosition : DetailsPanelPosition
    {
        public void TryBuyItem()
        {
            _storeManager.TryToBuyStoreGood(_item, GetAmount(), _price);
            SetDefaultAmount();
        }
    }
}
