using DoubleDCore.Configuration;
using DoubleDCore.GameResources.Base;
using Game.Gameplay.Survival_Meсhanics.Scripts.Common;
using Game.Quests.Base;
using Game.Tips;
using Game.Tips.Configs;
using UnityEngine;
using Zenject;

namespace Game.GameEngine.DI
{
    public class CommonGameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindConfigs();
            Container.BindInterfacesAndSelfTo<GameTrainingsStarter>().AsSingle().NonLazy();
        }
        
        private void BindConfigs()
        {
            var resourceContainer = Container.Resolve<IResourcesContainer>();
            var configsResource = resourceContainer.GetResource<ConfigsResource>();
            
            GameTrainingsConfig trainingsConfig = configsResource.GetConfig<GameTrainingsConfig>();
            
            Container.BindInstance(trainingsConfig).AsSingle();
        }
    }
}