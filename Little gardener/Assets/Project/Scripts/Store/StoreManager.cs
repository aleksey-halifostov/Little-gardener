using UnityEngine;
using System.Collections.Generic;
using LittleGardener.Inventory;
using LittleGardener.ItemsData;
using LittleGardener.Wallet;
using LittleGardener.ItemsBehaviour;

namespace LittleGardener.Store
{
    [RequireComponent(typeof(StoreStateSwitcher))]
    public class StoreManager : MonoBehaviour
    {
        private InventoryManager _inventoryManager;
        private WalletManager _walletManager;
        private StoreState _storeState;
        private Dictionary<GameItem, int> _itemsToSell;
        private AudioSource _uiAudioSource;

        [SerializeField] private AudioClip _storeSound;

        [SerializeField] private Transform _storeContent;
        [SerializeField] private Transform _detailsContent;

        [SerializeField] private GameObject _buyPositionPrefab;
        [SerializeField] private GameObject _sellPositionPrefab;
        [SerializeField] private GameObject _storePositionPrefab;

        [SerializeField] private ItemData[] _itemsToBuy;

        private void Awake()
        {
            _inventoryManager = FindFirstObjectByType<InventoryManager>();
            _walletManager = FindFirstObjectByType<WalletManager>();
            _uiAudioSource = GetComponentInParent<AudioSource>();
        }

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
            _itemsToSell = _inventoryManager.GetItemsForSale();

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
            if (!_walletManager.IsMoneyEnough(price * amount))
            {
                _walletManager.ShowMessage();
                return;
            }

            if (!_inventoryManager.TryGetFreeSlotsFor(item, amount, out List<InventorySlot> slots))
            {
                _inventoryManager.ShowMessage();
                return;
            }

            _inventoryManager.AddItem(item, amount, slots);
            _walletManager.SpendMoney(price * amount);
            _uiAudioSource.PlayOneShot(_storeSound);
            ClearDetails();
        }

        public void SellInventoryItem(GameItem item, int amount, int price)
        {
            if (!_inventoryManager.TryGetSlotsWith(item, amount, out List<InventorySlot> slots))
                return;

            _inventoryManager.RemoveItemByType(item, amount, slots);
            _walletManager.AddMoney(price * amount);
            _uiAudioSource.PlayOneShot(_storeSound);
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
