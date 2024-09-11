using System;
using System.Collections.Generic;
using DoubleDCore.Extensions;
using UnityEngine;
using Zenject;

namespace DoubleDCore.PhysicsTools.Casting.Raycasting
{
    public class RayCaster : IRayCaster, IFixedTickable
    {
        private bool _isActive;

        private readonly List<TargetListenerInfo> _targetsInfo = new();

        public bool IsActive => _isActive;

        public void AddListener(ITargetListener listener, RayCastInfo castInfo, Predicate<Collider> isTargetCondition)
        {
            if (_targetsInfo.Contains(t => t.Listener == listener))
            {
                Debug.LogError($"Ray listener {listener.GetType().Name} already exists");
                return;
            }

            _targetsInfo.Add(new TargetListenerInfo(listener, castInfo, isTargetCondition));
        }

        public void RemoveListener(ITargetListener listener)
        {
            _targetsInfo.Remove(t => t.Listener == listener);
        }

        public void StartCast()
        {
            _isActive = true;
        }

        public void StopCast()
        {
            _isActive = false;
        }

        public void FixedTick()
        {
            if (IsActive == false)
                return;

            foreach (var listenerInfo in _targetsInfo)
            {
                bool hasHitInfo = Physics.Raycast(listenerInfo.RayInfo.Ray, out RaycastHit hitInfo,
                    listenerInfo.RayInfo.RayMaxDistance, listenerInfo.RayInfo.Mask,
                    listenerInfo.RayInfo.QueryTriggerInteraction);

                if (hasHitInfo == false || listenerInfo.IsTargetCondition(hitInfo.collider) == false)
                {
                    if (listenerInfo.IsStay)
                    {
                        listenerInfo.IsStay = false;
                        listenerInfo.Listener.OnCastExit(listenerInfo.LastCollider);
                    }

                    continue;
                }

                if (listenerInfo.LastCollider != null && listenerInfo.LastCollider != hitInfo.collider)
                {
                    listenerInfo.IsStay = false;
                    listenerInfo.Listener.OnCastExit(listenerInfo.LastCollider);
                }

                if (listenerInfo.IsStay)
                    continue;

                listenerInfo.IsStay = true;
                listenerInfo.LastCollider = hitInfo.collider;
                listenerInfo.Listener.OnCastEnter(hitInfo.collider);
            }
        }

        private class TargetListenerInfo
        {
            public readonly ITargetListener Listener;
            public readonly RayCastInfo RayInfo;
            public readonly Predicate<Collider> IsTargetCondition;
            public Collider LastCollider;
            public bool IsStay = false;

            public TargetListenerInfo(ITargetListener targetListener, RayCastInfo rayInfo,
                Predicate<Collider> isTargetCondition)
            {
                Listener = targetListener;
                RayInfo = rayInfo;
                IsTargetCondition = isTargetCondition;
            }
        }
    }
}