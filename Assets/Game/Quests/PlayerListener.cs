using DoubleDCore.PhysicsTools.CollisionImpacts;
using UnityEngine;

namespace Game.Quests
{
    public class PlayerListener : TriggerListener<CharacterMover>
    {
        protected override bool IsTarget(Collider col, out CharacterMover target)
        {
            return col.TryGetComponent(out target);
        }

        protected override void OnTriggerStart(CharacterMover target)
        {
        }

        protected override void OnTriggerEnd(CharacterMover target)
        {
        }
    }
}