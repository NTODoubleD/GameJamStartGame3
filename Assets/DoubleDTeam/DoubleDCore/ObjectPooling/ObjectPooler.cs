using System;
using System.Collections.Generic;
using System.Linq;
using DoubleDCore.ObjectPooling.Base;
using DoubleDCore.ObjectPooling.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DoubleDCore.ObjectPooling
{
    public class ObjectPooler : IObjectPooler
    {
        private readonly Dictionary<Type, PoolSlot> _pool = new();

        public bool Contains<T>() where T : PoolingObject
        {
            return _pool.ContainsKey(typeof(T));
        }

        private bool Contains(Type poolingObjectType)
        {
            return _pool.ContainsKey(poolingObjectType);
        }

        public void Register(PoolingObjectCreateInfo createInfo)
        {
            PoolingObject prefab = createInfo.Prefab;
            int amountObjects = createInfo.SpawnAmount;
            Transform parent = createInfo.Parent;

            Type poolingType = prefab.GetType();

            if (Contains(poolingType))
            {
                var currentSlot = _pool[poolingType];

                currentSlot.CreateInfo = createInfo;

                amountObjects -= currentSlot.Stack.Count;
            }
            else
            {
                _pool.Add(poolingType, new PoolSlot(createInfo));
            }

            var slot = _pool[poolingType];

            for (int i = 0; i < amountObjects; i++)
            {
                var inst = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);

                slot.Stack.Push(inst);
                slot.CreateAmount++;

                inst.Initialize();
                inst.gameObject.SetActive(false);
            }
        }

        public void Remove<T>() where T : PoolingObject
        {
            Remove(typeof(T));
        }

        private void Remove(Type poolingType)
        {
            if (Contains(poolingType) == false)
                return;

            foreach (var poolingObject in _pool[poolingType].Stack)
                Object.Destroy(poolingObject);

            _pool.Remove(poolingType);
        }

        public T Get<T>() where T : PoolingObject
        {
            Type gettableType = typeof(T);

            if (Contains<T>() == false)
                throw new Exception($"Type {gettableType.Name} not registered");

            var slot = _pool[gettableType];

            if (slot.Stack.Count <= 0)
            {
                var oldCreateInfo = slot.CreateInfo;
                var newCreateInfo = new PoolingObjectCreateInfo(oldCreateInfo.Prefab,
                    oldCreateInfo.SpawnAmount * 2, oldCreateInfo.Parent);

                Register(newCreateInfo);

                Debug.LogWarning($"Pooler create object {gettableType.Name} in runtime, " +
                                 $"amount objects - {slot.CreateAmount}");
            }

            var poolingObject = slot.Stack.Pop();

            Spawn(poolingObject);

            return poolingObject as T;
        }

        public void Return(PoolingObject poolingObject)
        {
            DeSpawn(poolingObject);

            Type poolingType = poolingObject.GetType();

            if (Contains(poolingType) == false)
                return;

            _pool[poolingType].Stack.Push(poolingObject);
        }

        private void Spawn(PoolingObject poolingObject)
        {
            poolingObject.transform.SetParent(null);

            poolingObject.gameObject.SetActive(true);
            poolingObject.Spawn();
        }

        private void DeSpawn(PoolingObject poolingObject)
        {
            var createInfo = GetCreateInfo(poolingObject);

            poolingObject.DeSpawn();

            poolingObject.gameObject.SetActive(false);

            Transform transform = poolingObject.transform;
            transform.position = Vector3.zero;
            transform.SetParent(createInfo.Parent);
        }

        private PoolingObjectCreateInfo GetCreateInfo(PoolingObject poolingObject)
        {
            var poolingType = poolingObject.GetType();
            return Contains(poolingType) ? _pool[poolingType].CreateInfo : null;
        }

        public void Clear()
        {
            foreach (var key in _pool.Keys.ToArray())
                Remove(key);
        }

        private class PoolSlot
        {
            public Stack<PoolingObject> Stack { get; }

            public PoolingObjectCreateInfo CreateInfo { get; set; }

            public int CreateAmount { get; set; }

            public PoolSlot(PoolingObjectCreateInfo createInfo)
            {
                Stack = new Stack<PoolingObject>();
                CreateInfo = createInfo;
            }
        }
    }
}