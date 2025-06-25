using UnityEngine;

namespace LittleGardener.ItemsData
{
    [CreateAssetMenu(menuName = "Inventory/Items/Seeds/Create new Seeds")]
    public class SeedsData : ItemData
    {
        [SerializeField] private PlantData _plant;

        public PlantData Plant => _plant;
    }
}
