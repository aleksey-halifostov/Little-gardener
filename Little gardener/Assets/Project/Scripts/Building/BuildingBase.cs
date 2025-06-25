using UnityEngine;
using LittleGardener.BuildingsData;

namespace LittleGardener.Building
{
    public class BuildingBase : MonoBehaviour
    {
        [SerializeField] private BuildingData _data;

        public BuildingData Data => _data;
    }
}