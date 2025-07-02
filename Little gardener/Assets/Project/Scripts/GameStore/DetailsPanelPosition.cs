using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using LittleGardener.ItemsBehaviour;

namespace LittleGardener.GameStore
{
    public class DetailsPanelPosition : MonoBehaviour
    {
        protected GameItem _item;
        protected Store _storeManager;
        protected int _price;

        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _priceText;

        protected int GetAmount()
        {
            return int.Parse(_amountText.text);
        }

        protected void SetDefaultAmount()
        {
            _priceText.text = _price.ToString();
            _amountText.text = "1";
        }

        public void Init(GameItem item, int price, Store storeManager)
        {
            _item = item;
            _image.overrideSprite = item.Data.UISprite;
            _storeManager = storeManager;
            _price = price;
            SetDefaultAmount();
        }

        public void AmountDecrement()
        {
            if (GetAmount() == 1)
            {
                return;
            }

            _amountText.text = (GetAmount() - 1).ToString();
            _priceText.text = (_price * GetAmount()).ToString();
        }

        public virtual void AmountIncrement()
        {
            _amountText.text = (GetAmount() + 1).ToString();
            _priceText.text = (_price * GetAmount()).ToString();
        }
    }
}
