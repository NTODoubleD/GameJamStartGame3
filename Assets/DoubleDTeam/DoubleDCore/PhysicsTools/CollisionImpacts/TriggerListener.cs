using System;
using UnityEngine;

namespace DoubleDCore.PhysicsTools.CollisionImpacts
{
    [RequireComponent(typeof(Collider))]
    public abstract class TriggerListener<TTargetType> : MonoBehaviour
    {
        public event Action<TTargetType> TriggerEnter;
        public event Action<TTargetType> TriggerExit;

        public void OnTriggerEnter(Collider other)
        {
            if (IsTarget(other, out var target) == false)
                return;

            OnTriggerStart(target);

            TriggerEnter?.Invoke(target);
        }

        public void OnTriggerExit(Collider other)
        {
            if (IsTarget(other, out var target) == false)
                return;

            OnTriggerEnd(target);

            TriggerExit?.Invoke(target);
        }

        protected abstract bool IsTarget(Collider col, out TTargetType target);
        protected abstract void OnTriggerStart(TTargetType target);
        protected abstract void OnTriggerEnd(TTargetType target);
    }
}