using UnityEngine;

namespace LittleGardener.ItemsData
{
    public class ToolData : DurabilityItemData
    {
        [SerializeField] private AudioClip _toolSound;

        public AudioClip ToolSound => _toolSound;
    }
}