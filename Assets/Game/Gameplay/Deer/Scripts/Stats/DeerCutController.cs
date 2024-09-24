using DoubleDCore.Configuration;
using DoubleDCore.GameResources.Base;
using Game.Infrastructure.Storage;
using DoubleDCore.Service;
using Game.Gameplay.DayCycle;
using Game.Gameplay.Scripts.Configs;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Deers
{
    public class DeerCutController : MonoService
    {
        private const float HUNGER_STEP = 0.2f;
        
        [SerializeField] private CharacterAnimatorController _characterAnimatorController;

        private ItemStorage _storage;
        private DeerCutConfig _cutConfig;
        private DayCycleController _dayCycleController;

        [Inject]
        private void Init(ItemStorage storage, IResourcesContainer resourcesContainer, DayCycleController dayCycleController)
        {
            _storage = storage;
            _cutConfig = resourcesContainer.GetResource<ConfigsResource>().GetConfig<DeerCutConfig>();
            _dayCycleController = dayCycleController;
        }

        public bool CanCut(Deer deer)
        {
            return deer.DeerInfo.IsDead;
        }

        public void Cut(Deer deer)
        {
            if (CanCut(deer))
            {
                _characterAnimatorController.AnimateCutting(() => ApplyCut(deer));
            }
            else
            {
                Debug.LogError("CAN'T CUT THIS DEER");
            }
        }

        private void ApplyCut(Deer deer)
        {
            ProcessLoot(deer.DeerInfo);
            deer.Cut();
        }
        
        private void ProcessLoot(DeerInfo deerInfo)
        {
            float hungerDegree = deerInfo.HungerDegree;

            if (Mathf.Approximately(hungerDegree, 1) && deerInfo.DieDay != _dayCycleController.CurrentDay)
                hungerDegree -= HUNGER_STEP;

            var loot = _cutConfig.GetLoot(hungerDegree);
            
            foreach (var lootInfo in loot.Keys)
                _storage.AddItems(lootInfo, loot[lootInfo]);
        }
    }
}