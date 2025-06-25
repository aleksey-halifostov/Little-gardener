using UnityEngine;
using System.Collections.Generic;
using LittleGardener.Building;
using LittleGardener.BuildingsData;

namespace LittleGardener.Garden
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Fence : BuildingBase, IConnectable
    {
        private CellMap _map;
        private Vector2Int _myCell;
        private SpriteRenderer _spriteRenderer; 
        private Dictionary<int, Sprite> _states = new Dictionary<int, Sprite>();
        
        [SerializeField] private ConnectionData _connectionData;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _map = FindFirstObjectByType<CellMap>();

            int i = 0;

            foreach (Sprite sprite in _connectionData.Sprites)
            {
                if (sprite == null)
                    throw new System.ArgumentException(nameof(sprite));

                _states.Add(i++, sprite);
            }
        }

        private int GetState(Vector2Int myCell, Dictionary<Vector2Int, BuildingBase> neighbors)
        {
            int state = 0;

            foreach (KeyValuePair<Vector2Int, BuildingBase> neighbor in neighbors)
            {
                if (neighbor.Key.x > myCell.x && neighbor.Key.y == myCell.y)
                    state |= 1;
                else if (neighbor.Key.x < myCell.x && neighbor.Key.y == myCell.y)
                    state |= 2;
                else if (neighbor.Key.y < myCell.y && neighbor.Key.x > myCell.x)
                    state |= 4;
                else if (neighbor.Key.y > myCell.y && neighbor.Key.x < myCell.x)
                    state |= 8;
                else throw new System.ArgumentException(nameof(neighbors));
            }

            return state;
        }

        private Dictionary<Vector2Int, BuildingBase> UpdateConnections()
        {
            Dictionary<Vector2Int, BuildingBase> neighbors = _map.GetNeighboringCellsWithSameBuilding(_myCell, BuildingType.Fence);

            _spriteRenderer.sprite = _states[GetState(_myCell, neighbors)];

            return neighbors;
        }


        public void Connect()
        {
            _myCell = FindFirstObjectByType<CellGrid>().GetCell(transform.position.x, transform.position.y);

            foreach (BuildingBase building in UpdateConnections().Values)
            {
                (building as IConnectable).RefreshConnections();
            }
        }

        public void RefreshConnections()
        {
            UpdateConnections();
        }
    }
}