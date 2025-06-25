using UnityEngine;
using LittleGardener.PlayerControls;
using LittleGardener.SpriteSorting;

namespace LittleGardener.Building
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(ISortable))]
    public class TouchPlacer : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private CellMap _map;
        private CellGrid _grid;
        private ISortable _sortable;
        private Vector2Int _cell;

        public Vector2Int Cell => _cell;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _sortable = GetComponent<ISortable>();

            _grid = FindFirstObjectByType<CellGrid>();
            _map = FindFirstObjectByType<CellMap>();

            TouchFollow(transform.position);
        }

        private void Update()
        {
            if (InputManager.TryGetTouchWorldPosition(out Vector2 pos))
                TouchFollow(pos);
        }

        private void TouchFollow(Vector2 worldPos)
        {
            _cell = _grid.GetCell(worldPos.x, worldPos.y);
            transform.position = _grid.CellToWorld(_cell.x, _cell.y);
            _spriteRenderer.sortingOrder = -(int)(transform.position.y * 10) + 1;
            _spriteRenderer.color = PlacingColorFilter(_cell);
        }

        private Color PlacingColorFilter(Vector2Int cell)
        {
            if (_map.IsCellFree(cell))
            {
                return new Color(0f, 1f, .3f, .8f);
            }
            else
            {
                return new Color(1f, .2f, .2f, .8f);
            }
        }

        public void Init()
        {
            _sortable.Sort();

            if (TryGetComponent<IConnectable>(out IConnectable connectable))
            {
                connectable.Connect();
            }
        }
    }
}
