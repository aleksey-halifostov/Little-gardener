using UnityEngine;

namespace LittleGardener.ItemsData
{
    [CreateAssetMenu(menuName = "Inventory/Items/Tools/Create new Tool")]
    public class ToolData : ItemData
    {
        [SerializeField] private AudioClip _toolSound;
        [SerializeField] private AudioClip _destroySound;
        [SerializeField, Min(1)] private int _maxDurability;
        [SerializeField] private ToolType _toolType;

        public AudioClip ToolSound => _toolSound;
        public int MaxDurability => _maxDurability;
        public AudioClip DestroySound => _destroySound;
        public ToolType ToolType => _toolType;
    }
}