using UnityEngine;
using System.Collections.Generic;
using LittleGardener.BuildingsData;

namespace LittleGardener.Building
{
    public class CellMap : MonoBehaviour
    {
        private Dictionary<Vector2Int, BuildingBase> _places = new Dictionary<Vector2Int, BuildingBase>();

        public void OccupyCell(Vector2Int cell, BuildingBase building)
        {
            _places.Add(cell, building);
        }

        public bool IsCellFree(Vector2Int targetCell)
        {
            return !_places.ContainsKey(targetCell);
        }

        public Dictionary<Vector2Int, BuildingBase> GetNeighboringCellsWithSameBuilding(Vector2Int cell, BuildingType type)
        {
            Dictionary<Vector2Int, BuildingBase> buildings = new Dictionary<Vector2Int, BuildingBase>();

            Vector2Int[] directions = new Vector2Int[]
            {
                new Vector2Int(-1, 1),
                new Vector2Int(1, -1),
                new Vector2Int(1, 0),
                new Vector2Int(-1, 0)
            };

            foreach (Vector2Int dir in directions)
            {
                Vector2Int neighbor = cell + dir;

                if (!IsCellFree(neighbor) && _places[neighbor].Data.BuildingType == type)
                {
                    buildings.Add(neighbor, _places[neighbor]);
                }
            }

            return buildings;
        }
    }
}
