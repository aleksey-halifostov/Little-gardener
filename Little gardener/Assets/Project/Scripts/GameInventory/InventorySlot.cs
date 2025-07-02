using UnityEngine;
using LittleGardener.ItemsBehaviour;

namespace LittleGardener.GameInventory
{
    [RequireComponent(typeof(InventorySlotView))]
    public class InventorySlot : MonoBehaviour
    {
        private GameItem _item;
        private int _amount;
        private InventorySlotView _view;

        public bool IsFree => _item == null;

        private void Awake()
        {
            _view = GetComponent<InventorySlotView>();
        }

        private void OccupySlot(GameItem item, int amount)
        {
            _item = item;
            _amount += amount;
            _view.SetItem(item.Data.UISprite);

            if (item is ToolItem tool)
            {
                int max = tool.MaxDurability;
                int current = tool.CurrentDurability;

                _view.ShowDurability();
                SetCurrentDurability(max, current);
            }

            _view.UpdateText(amount.ToString());
        }

        public void ClearSlot()
        {
            _amount = 0;
            _item = null;
            _view.RemoveItem();
        }

        public void RemoveItem(int removeAmount)
        {
            if (removeAmount > _amount)
                throw new System.ArgumentOutOfRangeException(nameof(removeAmount));

            if (removeAmount == _amount)
            {
                ClearSlot();
                return;
            }

            _amount -= removeAmount;
            _view.UpdateText(_amount.ToString());
        }

        public void AddItem(GameItem item, int amount)
        {
            if (item == null)
                throw new System.ArgumentNullException(nameof(item));
            if (!IsFree && item.Data.ItemName != _item.Data.ItemName)
                throw new System.ArgumentException(nameof(item), $"InventorySlot: Got incorrect item: " +
                    $"{item.Data.ItemName} but {_item.Data.ItemName} expected.");
            if (amount < 0 || amount > item.Data.MaximumAmount - _amount)
                throw new System.ArgumentOutOfRangeException(nameof(amount));

            if (IsFree)
            {
                OccupySlot(item, amount);
            }
            else
            {
                _amount += amount;
                _view.UpdateText(_amount.ToString());
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

        public void SetCurrentDurability(int max, int current)
        {
            _view.UpdateDurability(max, current);
        }
    }
}
