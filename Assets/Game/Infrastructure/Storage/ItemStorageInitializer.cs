using DoubleDTeam.Containers;
using DoubleDTeam.Containers.Base;
using UnityEngine;

namespace Game.Infrastructure.Storage
{
    public class ItemStorageInitializer : InitializeObject
    {
        [SerializeField] private ItemStorageInfo _testInfo;

        public override void Deinitialize()
        {
            Services.ProjectContext.RemoveModule<ItemStorage>();

        }

        public override void Initialize()
        {
            Services.ProjectContext.RegisterModule(new ItemStorage(_testInfo));
        }
    }
}