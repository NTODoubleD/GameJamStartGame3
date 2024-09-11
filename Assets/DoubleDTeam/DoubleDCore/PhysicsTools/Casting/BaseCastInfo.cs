using UnityEngine;

namespace DoubleDCore.PhysicsTools.Casting
{
    public class BaseCastInfo
    {
        public readonly LayerMask Mask;

        public readonly QueryTriggerInteraction QueryTriggerInteraction;

        public BaseCastInfo(LayerMask mask,
            QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
        {
            Mask = mask;
            QueryTriggerInteraction = queryTriggerInteraction;
        }
    }
}