using System;
using UnityEngine;

namespace DoubleDCore.PhysicsTools.Casting
{
    public interface ICaster<in TDataType> where TDataType : BaseCastInfo
    {
        public bool IsActive { get; }

        public void AddListener(ITargetListener listener, TDataType castInfo, Predicate<Collider> isTargetCondition);

        public void RemoveListener(ITargetListener listener);

        public void StartCast();

        public void StopCast();
    }
}