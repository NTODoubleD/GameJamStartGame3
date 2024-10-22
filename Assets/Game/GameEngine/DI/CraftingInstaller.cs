using Cinemachine;
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
            
            Container.Bind<CraftController>().AsSingle();
            Container.Bind<CookingController>().AsSingle();
        }
    }
}