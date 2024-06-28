using System.Collections.Generic;
using DoubleDTeam.Containers;
using DoubleDTeam.Containers.Base;
using DoubleDTeam.ObjectPooling.Base;
using DoubleDTeam.ObjectPooling.Data;
using UnityEngine;

namespace DoubleDTeam.ObjectPooling
{
    public class ObjectPoolerInitializer : InitializeObject
    {
        [SerializeField] private List<PoolingObjectCreateInfo> _poolInfo;

        public override void Initialize()
        {
            IObjectPooler pooler = new ObjectPooler();
            Services.SceneContext.RegisterModule(pooler);

            foreach (var createInfo in _poolInfo)
                pooler.Register(createInfo);
        }

        public override void Deinitialize()
        {
            var objectPooler = Services.SceneContext.RemoveModule<IObjectPooler>();

            objectPooler.Clear();
        }
    }
}