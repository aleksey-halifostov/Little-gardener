using UnityEngine;

namespace LittleGardener.BuildingsData
{
    [CreateAssetMenu(menuName = "Building/new Building")]
    public class BuildingData : ScriptableObject
    {
        [SerializeField] private int _price;
        [SerializeField] private BuildingType _buildingType;
        [SerializeField] private Sprite _uiSprite;
        [SerializeField] private GameObject _objectPrefab;

        public int Price => _price;
        public BuildingType BuildingType => _buildingType;
        public Sprite UISprite => _uiSprite;
        public GameObject ObjectPrefab => _objectPrefab;
    }
}