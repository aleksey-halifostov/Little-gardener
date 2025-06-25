using UnityEngine;
using LittleGardener.BuildingsData;

namespace LittleGardener.Building
{
    public class BuildStoreFiller : MonoBehaviour
    {
        [SerializeField] private Transform _buildStoreContent;
        [SerializeField] private GameObject _buildStorePositionPrefab;
        [SerializeField] private BuildingData[] _buildingItems;

        private void Awake()
        {
            FillStoreContent();
        }

        private void FillStoreContent()
        {
            foreach (BuildingData item in _buildingItems)
            {
                GameObject slot = Instantiate(_buildStorePositionPrefab);
                slot.transform.SetParent(_buildStoreContent, false);
                slot.GetComponent<BuildingStorePosition>().Init(item);
            }
        }
    }
}
