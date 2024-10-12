using System;
using UnityEngine;

namespace DoubleDCore.PhysicsTools.CollisionImpacts
{
    [RequireComponent(typeof(Collider))]
    public abstract class TriggerListener<TTargetType> : MonoBehaviour where TTargetType : class
    {
        public event Action<TTargetType> TriggerEnter;
        public event Action<TTargetType> TriggerExit;

        private TTargetType _currentTarget;

        public void OnTriggerEnter(Collider other)
        {
            if (IsTarget(other, out var target) == false)
                return;
            
            _currentTarget = target;
            OnTriggerStart(target);

            TriggerEnter?.Invoke(target);
        }

        public void OnTriggerExit(Collider other)
        {
            if (IsTarget(other, out var target) == false)
                return;

            _currentTarget = null;
            OnTriggerEnd(target);

            TriggerExit?.Invoke(target);
        }

        public bool IsTargetInTrigger()
        {
            return _currentTarget != null;
        }

        protected abstract bool IsTarget(Collider col, out TTargetType target);
        protected abstract void OnTriggerStart(TTargetType target);
        protected abstract void OnTriggerEnd(TTargetType target);
    }
}