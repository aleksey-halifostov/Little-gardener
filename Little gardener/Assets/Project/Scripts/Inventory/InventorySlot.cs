using UnityEngine;
using TMPro;
using UnityEngine.UI;
using LittleGardener.ItemsBehaviour;
using LittleGardener.ItemsData;

namespace LittleGardener.Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        private GameItem _item;
        private int _amount;

        [SerializeField] private InventoryItemIcon _icon;
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private Slider _slider;

        public bool IsFree { get; private set; } = true;

        private void UpdateText()
        {
            if (_item == null || _item.Data.HasDurability)
            {
                _amountText.text = "";
                return;
            }

            _amountText.text = _amount.ToString();
        }
        private void OccupySlot(GameItem item, int amount)
        {
            _item = item;
            _amount += amount;
            _icon.SetItem(item.Data.UISprite);
            IsFree = false;

            if (item.Data.HasDurability)
            {
                int max = ((DurabilityItemData)item.Data).MaxDurability;
                int current = ((ItemWithDurability)item).CurrentDurability;

                _slider.gameObject.SetActive(true);
                ShowCurrentDurability(max, current);
            }

            UpdateText();
        }

        public void ClearSlot()
        {
            _amount = 0;
            _item = null;
            _icon.RemoveItem();
            _slider.gameObject.SetActive(false);
            IsFree = true;
            UpdateText();
        }

        public void RemoveItem(int removeAmount)
        {
            if (removeAmount == _amount)
            {
                ClearSlot();
                return;
            }

            _amount -= removeAmount;
            UpdateText();

        }


        public void AddItem(GameItem item, int amount)
        {
            if (IsFree && item != null)
            {
                OccupySlot(item, amount);
            }
            else if (item.Data == _item.Data)
            {
                _amount += amount;
                UpdateText();
            }
        }


        public GameItem GetItem()
        {
            return _item;
        }

        public int GetItemAmount()
        {
            return _amount;
        }

        public void ShowCurrentDurability(int max, int current)
        {
            if (current > max || current < 0)
                throw new System.ArgumentOutOfRangeException(nameof(current));

            _slider.maxValue = max;
            _slider.value = current;
        }
    }

}
