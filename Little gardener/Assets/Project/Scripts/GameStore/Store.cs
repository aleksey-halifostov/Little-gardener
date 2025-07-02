using UnityEngine;
using System.Collections.Generic;
using LittleGardener.GameInventory;
using LittleGardener.GameWallet;
using LittleGardener.ItemsBehaviour;
using LittleGardener.GameManagement;
using LittleGardener.ItemsData;

namespace LittleGardener.GameStore
{
    public class Store : MonoBehaviour
    {
        private StoreState _storeState;
        private Dictionary<GameItem, int> _itemsToSell;

        [SerializeField] private Wallet _walletManager;
        [SerializeField] private AudioClip _storeSound;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private Inventory _inventory;

        [SerializeField] private Transform _storeContent;
        [SerializeField] private Transform _detailsContent;

        [SerializeField] private GameObject _buyPositionPrefab;
        [SerializeField] private GameObject _sellPositionPrefab;
        [SerializeField] private GameObject _storePositionPrefab;

        [SerializeField] private ItemData[] _itemsToBuy;

        private void OnEnable()
        {
            StorePosition.OnPositionClicked += ShowDetails;
        }

        private void OnDisable()
        {
            ClearStore();
            StorePosition.OnPositionClicked -= ShowDetails;
        }

        private void FillFromStore()
        {
            _storeState = StoreState.Buy;

            foreach (ItemData data in _itemsToBuy)
            {
                GameItem item = ItemFactory.CreateNewItem(data);
                GameObject slot = Instantiate(_storePositionPrefab);
                slot.transform.SetParent(_storeContent, false);
                slot.GetComponent<StorePosition>().Init(item, PriceCalculator.GetBuyPrice(item));
            }
        }

        private void FillFromInventory()
        {
            _storeState = StoreState.Sell;
            _itemsToSell = _inventory.GetItemsForSale();

            foreach (GameItem item in _itemsToSell.Keys)
            {
                GameObject slot = Instantiate(_storePositionPrefab);
                slot.transform.SetParent(_storeContent, false);
                slot.GetComponent<StorePosition>().Init(item, PriceCalculator.GetSellPrice(item));
            }
        }

        private void ClearStore()
        {
            for (int i = 0; i < _storeContent.childCount; i++)
            {
                Destroy(_storeContent.GetChild(i).gameObject);
            }

            ClearDetails();
        }

        private void ClearDetails()
        {
            if (_detailsContent.childCount > 0)
            {
                Destroy(_detailsContent.GetChild(0).gameObject);
            }
        }

        private void ShowDetails(GameItem item, int price)
        {
            if (item == null)
                throw new System.ArgumentNullException(nameof(item));
            if (price < 0)
                throw new System.ArgumentNullException(nameof(item));

            ClearDetails();

            switch (_storeState)
            {
                case StoreState.Buy:
                    GameObject buySlot = Instantiate(_buyPositionPrefab);
                    buySlot.transform.SetParent(_detailsContent, false);
                    buySlot.GetComponent<StoreBuyPosition>().Init(item, price, this);
                    break;
                case StoreState.Sell:
                    GameObject sellSlot = Instantiate(_sellPositionPrefab);
                    sellSlot.transform.SetParent(_detailsContent, false);
                    sellSlot.GetComponent<StoreSellPosition>().Init(item, price, this, _itemsToSell[item]);
                    break;
            }
        }

        public void TryToBuyStoreGood(GameItem item, int amount, int price)
        {
            if (item == null)
                throw new System.ArgumentNullException(nameof(item));
            if (amount < 1)
                throw new System.ArgumentOutOfRangeException(nameof(amount));
            if (price < 0)
                throw new System.ArgumentOutOfRangeException(nameof(price));

            if (!_walletManager.IsMoneyEnough(price * amount))
            {
                _walletManager.ShowMessage();
                return;
            }

            if (!_inventory.TryGetFreeSlotsFor(item, amount, out List<InventorySlot> slots))
            {
                _inventory.ShowMessage();
                return;
            }

            _inventory.AddItem(item, amount, slots);
            _walletManager.SpendMoney(price * amount);
            _audioManager.PlayUISound(_storeSound);
            ClearStore();
            FillFromStore();
        }

        public void SellInventoryItem(GameItem item, int amount, int price)
        {
            if (item == null)
                throw new System.ArgumentNullException(nameof(item));
            if (amount < 1)
                throw new System.ArgumentOutOfRangeException(nameof(amount));
            if (price < 0)
                throw new System.ArgumentOutOfRangeException(nameof(price));

            if (!_inventory.TryGetSlotsWith(item, amount, out List<InventorySlot> slots))
                return;

            _inventory.RemoveItemByType(item, amount, slots);
            _walletManager.AddMoney(price * amount);
            _audioManager.PlayUISound(_storeSound);
            ClearStore();
            FillFromInventory();
        }

        public void SetBuyState()
        {
            ClearStore();
            FillFromStore();
            _storeState = StoreState.Buy;
        }

        public void SetSellState()
        {
            ClearStore();
            FillFromInventory();
            _storeState = StoreState.Sell;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
