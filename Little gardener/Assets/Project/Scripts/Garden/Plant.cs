using UnityEngine;

namespace LittleGardener.Garden
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Plant : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public void RemovePlant()
        {
            _spriteRenderer.sprite = null;
        }
    }
}
