using UnityEngine;
using UnityEngine.UI;

namespace LittleGardener.Inventory
{
    public class InventoryItemIcon : MonoBehaviour
    {
        [SerializeField] private Image _image;

        private void SetAlpha(float a)
        {
            Color color = _image.color;
            color.a = a;
            _image.color = color;
        }

        public void RemoveItem()
        {
            _image.overrideSprite = null;
            SetAlpha(0);
        }

        public void SetItem(Sprite sprite)
        {
            _image.overrideSprite = sprite;
            SetAlpha(1f);
        }
    }
}
