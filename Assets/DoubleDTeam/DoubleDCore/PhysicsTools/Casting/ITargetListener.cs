using UnityEngine;

namespace DoubleDCore.PhysicsTools.Casting
{
    public interface ITargetListener
    {
        public void OnCastEnter(Collider target);
        public void OnCastExit(Collider target);
    }
}