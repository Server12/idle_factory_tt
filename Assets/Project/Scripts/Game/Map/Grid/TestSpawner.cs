using System.Collections.Generic;
using Game.Extensions;
using Game.Map.Views.Buildings;
using NaughtyAttributes;
using UnityEngine;

namespace Game.Grid
{
    public class TestSpawner : MonoBehaviour
    {
        public IsometricGrid grid;

        public List<GameObject> buildingPrefabs;
        public Vector2Int[] cellPositions;

        private void Awake()
        {
            transform.DestroyChildren();
            Destroy(this);
        }

        [Button]
        private void Spawn()
        {
            transform.DestroyChildrenImmediate();
            for (int i = 0; i < cellPositions.Length; i++)
            {
                if (i < buildingPrefabs.Count)
                {
                    var building = Instantiate(buildingPrefabs[i], transform, true).GetComponent<BuildingView>();
                    //  building.transform.rotation = Quaternion.identity;
                    building.SetPosition(grid.GridToWorld(cellPositions[i]));
                }
            }
        }
    }
}