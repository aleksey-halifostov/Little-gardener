using UnityEngine;
using UnityEngine.UI;

namespace LittleGardener.GameStore
{
    [RequireComponent(typeof(Store))]
    public class StoreStateSwitcher : MonoBehaviour
    {
        private Store _storeManager;

        [SerializeField] private Image _buyButtonImage;
        [SerializeField] private Image _sellButtonImage;

        [SerializeField] private Sprite _buyButtonActive;
        [SerializeField] private Sprite _buyButtonInactive;

        [SerializeField] private Sprite _sellButtonActive;
        [SerializeField] private Sprite _sellButtonInactive;

        private void Awake()
        {
            _storeManager = GetComponent<Store>();
        }

        private void OnEnable()
        {
            SetBuyMode();
        }

        public void SetBuyMode()
        {
            _buyButtonImage.overrideSprite = _buyButtonActive;
            _sellButtonImage.overrideSprite = _sellButtonInactive;

            _storeManager.SetBuyState();
        }

        public void SetSellMode()
        {
            _buyButtonImage.overrideSprite = _buyButtonInactive;
            _sellButtonImage.overrideSprite = _sellButtonActive;

            _storeManager.SetSellState();
        }
    }
}
