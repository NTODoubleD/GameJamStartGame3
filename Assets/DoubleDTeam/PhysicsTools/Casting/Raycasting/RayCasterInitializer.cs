using DoubleDTeam.Containers;
using DoubleDTeam.Containers.Base;
using UnityEngine;

namespace DoubleDTeam.PhysicsTools.Casting.Raycasting
{
    public class RayCasterInitializer : InitializeObject
    {
        [SerializeField] private RayCaster _rayCaster;

        public override void Initialize()
        {
            Services.ProjectContext.RegisterModule(_rayCaster as IRayCaster);
        }

        public override void Deinitialize()
        {
            Services.ProjectContext.RemoveModule<IRayCaster>();
        }
    }
}