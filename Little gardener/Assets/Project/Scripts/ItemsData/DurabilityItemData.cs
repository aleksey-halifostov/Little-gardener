using UnityEngine;

namespace LittleGardener.ItemsData
{
    public class DurabilityItemData : ItemData
    {
        [SerializeField] private int _maxDurability;
        [SerializeField] private AudioClip _destroySound;
        public int MaxDurability  => _maxDurability;
        public AudioClip DestroySound => _destroySound;
    }
}
