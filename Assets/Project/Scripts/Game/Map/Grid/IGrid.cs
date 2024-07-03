using UnityEngine;

namespace Game.Grid
{
    public interface IGrid
    {
        Vector3 GridToWorld(Vector2Int gridPos);
        
        Vector2Int WorldToGrid(Vector3 worldPos);
    }
}