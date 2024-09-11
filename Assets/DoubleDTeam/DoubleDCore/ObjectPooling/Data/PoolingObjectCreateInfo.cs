using System;
using DoubleDCore.ObjectPooling.Base;
using UnityEngine;

namespace DoubleDCore.ObjectPooling.Data
{
    [Serializable]
    public class PoolingObjectCreateInfo
    {
        [SerializeField] private PoolingObject _prefab;
        [SerializeField, Min(1)] private int _spawnAmount;
        [SerializeField] private Transform _parent;

        public PoolingObject Prefab => _prefab;

        public int SpawnAmount => _spawnAmount;

        public Transform Parent => _parent;

        public PoolingObjectCreateInfo(PoolingObject prefab, int spawnAmount, Transform parent)
        {
            _prefab = prefab;
            _spawnAmount = spawnAmount;
            _parent = parent;
        }
    }
}