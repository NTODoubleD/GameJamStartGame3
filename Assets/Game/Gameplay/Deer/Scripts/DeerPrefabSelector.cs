using System;
using System.Linq;
using UnityEngine;

namespace Game.Gameplay.Scripts
{
    [Serializable]
    public class DeerPrefabSelector
    {
        [SerializeField] private DeerPrefab[] _prefabs;
            
        public Deer GetPrefab(DeerInfo info)
        {
            return _prefabs.First(x => x.Gender == info.Gender).Prefab;
        }

        [Serializable]
        private struct DeerPrefab
        {
            public GenderType Gender;
            public Deer Prefab;
        }
    }
}