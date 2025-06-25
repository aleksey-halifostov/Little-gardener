using UnityEngine;
using LittleGardener.GameManagement;

namespace LittleGardener.Building
{
    public class CellGrid : MonoBehaviour
    {
        private readonly Vector2 _baseVectorX = new Vector2(1f, 0.6f);
        private readonly Vector2 _baseVectorY = new Vector2(0f, 1.2f);

        private Vector2Int ClampCell(int x, int y)
        {
            Vector2 cellWorldPos = CellToWorld(x, y);

            if (cellWorldPos.y > WorldLimiter.YWorldMax)
                y--;
            else if (cellWorldPos.y < -WorldLimiter.YWorldMax)
                y++;

            return new Vector2Int(x, y);
        }

        public Vector2 CellToWorld(int x, int y)

        {
            float worldX = x * _baseVectorX.x + y * _baseVectorY.x;
            float worldY = x * _baseVectorX.y + y * _baseVectorY.y;

            return new Vector2(worldX, worldY);
        }

        public Vector2Int GetCell(float worldX, float worldY)
        {
            Vector2 clamped = WorldLimiter.ClampToWorldBounds(worldX, worldY);

            int cellX = Mathf.RoundToInt(clamped.x);
            int cellY = Mathf.RoundToInt((clamped.y - clamped.x * 0.6f) / 1.2f);

            return ClampCell(cellX, cellY);
        }
    }
}