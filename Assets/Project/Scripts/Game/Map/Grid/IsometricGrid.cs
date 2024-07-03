using System;
using UnityEngine;

namespace Game.Grid
{
    public class IsometricGrid : MonoBehaviour, IGrid
    {
        public int gridWidth = 10;
        public int gridHeight = 10;
        public float cellWidth = 1.0f;
        public float cellHeight = 0.5f;
        public float rotationAngle = 45.0f;
        
        private Vector3 GetIsoPosition(int x, int y)
        {
            float angleRad = Mathf.Deg2Rad * rotationAngle;
            float cosAngle = Mathf.Cos(angleRad);
            float sinAngle = Mathf.Sin(angleRad);

            float isoX = (x - y) * cellWidth * cosAngle - (x + y) * cellHeight * sinAngle;
            float isoZ = (x - y) * cellWidth * sinAngle + (x + y) * cellHeight * cosAngle;
            return new Vector3(isoX, 0, isoZ);
        }

        public Vector2Int WorldToGrid(Vector3 worldPos)
        {
            Vector3 localPos = worldPos - transform.position + GetIsoPosition(gridWidth / 2, gridHeight / 2);
            float angleRad = Mathf.Deg2Rad * rotationAngle;
            float cosAngle = Mathf.Cos(angleRad);
            float sinAngle = Mathf.Sin(angleRad);

            float invCellWidth = 1.0f / cellWidth;
            float invCellHeight = 1.0f / cellHeight;

            float localX = localPos.x * cosAngle + localPos.z * sinAngle;
            float localY = -localPos.x * sinAngle + localPos.z * cosAngle;

            int x = Mathf.RoundToInt((localX * invCellWidth + localY * invCellHeight) * 0.5f);
            int y = Mathf.RoundToInt((localY * invCellHeight - localX * invCellWidth) * 0.5f);

            return new Vector2Int(x, y);
        }

        public Vector3 GridToWorld(Vector2Int gridPos)
        {
            Vector3 worldPos = GetIsoPosition(gridPos.x, gridPos.y) - GetIsoPosition(gridWidth / 2, gridHeight / 2) +
                               new Vector3(transform.position.x, 0, transform.position.z);
            return worldPos;
        }


#if UNITY_EDITOR

        public Color gizmoColor;
        public bool drawGrid;

        void OnDrawGizmos()
        {
            if (!drawGrid) return;
            Gizmos.color = gizmoColor;

            Vector3 gridOffset = GetIsoPosition(gridWidth / 2, gridHeight / 2);

            for (int x = 0; x <= gridWidth; x++)
            {
                for (int y = 0; y <= gridHeight; y++)
                {
                    Vector3 startPos = GetIsoPosition(x, y) - gridOffset + transform.position;
                    Vector3 endPos1 = GetIsoPosition(x + 1, y) - gridOffset + transform.position;
                    Vector3 endPos2 = GetIsoPosition(x, y + 1) - gridOffset + transform.position;

                    if (x < gridWidth)
                    {
                        Gizmos.DrawLine(startPos, endPos1);
                    }

                    if (y < gridHeight)
                    {
                        Gizmos.DrawLine(startPos, endPos2);
                    }
                }
            }
        }
#endif
    }
}