using UnityEngine;

namespace DoubleDCore.PhysicsTools.Casting.Raycasting
{
    public class RayCastInfo : BaseCastInfo
    {
        public readonly float RayMaxDistance;
        public Ray Ray;

        public RayCastInfo(Ray ray, float rayMaxDistance, LayerMask mask,
            QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
            : base(mask, queryTriggerInteraction)
        {
            RayMaxDistance = rayMaxDistance;
            Ray = ray;
        }
    }
}