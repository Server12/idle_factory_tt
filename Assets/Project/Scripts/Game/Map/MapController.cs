using System;
using Game.Grid;
using UnityEngine;

namespace Game.Map.Controllers
{
    public interface IMapController
    {
        Transform BuildingsHolder { get; }

        IGrid Grid { get; }
    }

    public class MapController : MonoBehaviour, IMapController
    {
        private IGrid _grid;

        [SerializeField] private Transform _buildingsHolder;

        public Transform BuildingsHolder => _buildingsHolder;

        public IGrid Grid => _grid;

        private void Awake()
        {
            _grid = GetComponentInChildren<IGrid>();
        }
    }
}