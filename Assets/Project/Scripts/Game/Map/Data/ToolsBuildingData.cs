using System;
using Game.Data;
using UnityEngine;

namespace Game.Map.Data
{
    [Serializable]
    public struct ToolsCraftData
    {
        [SerializeField] private ResourceItemType _item1;
        [SerializeField] private ResourceItemType _item2;
        [SerializeField] private ResourceItemType _craftedItem;

        public ResourceItemType Item1 => _item1;

        public ResourceItemType Item2 => _item2;

        public ResourceItemType CraftedItem => _craftedItem;
    }
    
    [Serializable]
    public class ToolsBuildingData:BaseBuildingData
    {
        [SerializeField] private float _craftDelaySeconds = 1.5f;

        [SerializeField] private ToolsCraftData[] _craftRecipes;

        public float CraftDelaySeconds => _craftDelaySeconds;

        public ToolsCraftData[] CraftRecipes => _craftRecipes;
    }
}