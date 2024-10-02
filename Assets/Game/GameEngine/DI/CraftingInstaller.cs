using Cinemachine;
using DoubleDCore.Configuration;
using DoubleDCore.GameResources.Base;
using Game.Gameplay.Crafting;
using UnityEngine;
using Zenject;

namespace Game.GameEngine.DI
{
    public class CraftingInstaller : MonoInstaller
    {
        [SerializeField] private CinemachineVirtualCamera _characterCamera;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_characterCamera).AsCached();
            
            BindConfigs();

            Container.Bind<CraftController>().AsSingle();
            Container.Bind<CookingController>().AsSingle();
        }
        
        private void BindConfigs()
        {
            var resourceContainer = Container.Resolve<IResourcesContainer>();
            var configsResource = resourceContainer.GetResource<ConfigsResource>();
            
            CookingConfig cookingConfig = configsResource.GetConfig<CookingConfig>();
            
            Container.BindInstance(cookingConfig).AsSingle();
        }
    }
}