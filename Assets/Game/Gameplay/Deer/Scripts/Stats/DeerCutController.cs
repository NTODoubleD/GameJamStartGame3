using DoubleDCore.Configuration;
using DoubleDCore.GameResources.Base;
using Game.Infrastructure.Storage;
using Game.Gameplay.DayCycle;
using Game.Gameplay.Configs;
using UnityEngine;

namespace Game.Gameplay.Deers
{
    public class DeerCutController
    {
        private readonly CharacterAnimatorController _characterAnimatorController;
        private readonly ItemStorage _storage;
        private readonly DeerCutConfig _cutConfig;
        private readonly DayCycleController _dayCycleController;
        private readonly float _hungerStep;

        public DeerCutController(ItemStorage storage, IResourcesContainer resourcesContainer, 
            DayCycleController dayCycleController, CharacterAnimatorController characterAnimatorController,
            DeerHungerConfig deerHungerConfig)
        {
            _storage = storage;
            _cutConfig = resourcesContainer.GetResource<ConfigsResource>().GetConfig<DeerCutConfig>();
            _dayCycleController = dayCycleController;
            _characterAnimatorController = characterAnimatorController;
            _hungerStep = deerHungerConfig.HungerStep;
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
                hungerDegree -= _hungerStep;

            var loot = _cutConfig.GetLoot(hungerDegree);
            
            foreach (var lootInfo in loot.Keys)
                _storage.AddItems(lootInfo, loot[lootInfo]);
        }
    }
}