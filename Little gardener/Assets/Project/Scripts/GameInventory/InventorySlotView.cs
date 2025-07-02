using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LittleGardener.GameInventory
{
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private Image _itemIcon;
        [SerializeField] private Slider _durabilitySlider;
        [SerializeField] private TextMeshProUGUI _amountText;

        private void SetImageAlpha(float a)
        {
            Color color = _itemIcon.color;
            color.a = a;
            _itemIcon.color = color;
        }

        private void HideDurability()
        {
            _durabilitySlider.gameObject.SetActive(false);
        }

        public void RemoveItem()
        {
            _itemIcon.overrideSprite = null;
            SetImageAlpha(0);
            HideDurability();
            UpdateText("");
        }

        public void SetItem(Sprite sprite)
        {
            _itemIcon.overrideSprite = sprite;
            SetImageAlpha(1f);
        }

        public void ShowDurability()
        {
            _durabilitySlider.gameObject.SetActive(true);
        }

        public void UpdateDurability(int max, int current)
        {
            if (current > max || current < 0)
                throw new System.ArgumentOutOfRangeException(nameof(current));

            _durabilitySlider.maxValue = max;
            _durabilitySlider.value = current;
        }

        public void UpdateText(string text)
        {
            _amountText.text = text;
        }
    }
}
