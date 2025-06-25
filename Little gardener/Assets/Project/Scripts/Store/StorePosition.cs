using UnityEngine;
using TMPro;
using UnityEngine.UI;
using LittleGardener.ItemsBehaviour;
using System;

namespace LittleGardener.Store
{
    public class StorePosition : MonoBehaviour
    {
        private GameItem _item;
        private int _price;

        [SerializeField] private TextMeshProUGUI _itemName;
        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _itemPrice;

        public static Action<GameItem, int> OnPositionClicked;

        public void Init(GameItem item, int price)
        {
            _item = item;
            _itemImage.overrideSprite = item.Data.UISprite;
            _price = price;
            _itemName.text = item.Data.ItemName;
            _itemPrice.text = price.ToString();
        }

        public void ShowDetails()
        {
            OnPositionClicked?.Invoke(_item, _price);
        }
    }
}
