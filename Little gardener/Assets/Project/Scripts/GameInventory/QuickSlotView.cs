using UnityEngine;
using UnityEngine.UI;

namespace LittleGardener.GameInventory
{
    [RequireComponent(typeof(Image))]
    public class QuickSlotView : MonoBehaviour
    {
        private Image _image;
        private Color _activeColor = new Color(.65f, .5f, .3f, 1.0f);
        private Color _simpleColor = new Color(1f, 1f, 1f, 1f);

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void SetActiveColor()
        {
            _image.color = _activeColor;
        }

        public void SetInactiveColor()
        {
            _image.color = _simpleColor;
        }
    }
}