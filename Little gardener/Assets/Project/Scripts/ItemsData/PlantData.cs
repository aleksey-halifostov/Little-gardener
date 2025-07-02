using UnityEngine;

namespace LittleGardener.ItemsData
{
    [CreateAssetMenu(menuName = "Inventory/Items/Plants/Create new Plant")]
    public class PlantData : ItemData
    {
        [SerializeField, Min(1)] private int _growTime;
        [SerializeField] private Sprite[] _progressSprites;

        public int GrowTime => _growTime;
        public Sprite[] ProgressSprites  => _progressSprites;
    }
}
