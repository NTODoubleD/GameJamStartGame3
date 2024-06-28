using UnityEngine;

namespace DoubleDTeam.PhysicsTools.Casting
{
    public interface ITargetListener
    {
        public void OnCastEnter(Collider target);
        public void OnCastExit(Collider target);
    }
}