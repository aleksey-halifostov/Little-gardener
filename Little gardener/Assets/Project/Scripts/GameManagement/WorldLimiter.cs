using UnityEngine;

namespace LittleGardener.GameManagement
{
    public static class WorldLimiter
    {
        public const int XWorldMax = 15;
        public const int YWorldMax = 10;

        public static Vector3 ClampToWorldBounds(float x, float y, float z = 0)
        {
            return new Vector3(Mathf.Clamp(x, -XWorldMax, XWorldMax),
                               Mathf.Clamp(y, -YWorldMax, YWorldMax), z);
        }
    }
}