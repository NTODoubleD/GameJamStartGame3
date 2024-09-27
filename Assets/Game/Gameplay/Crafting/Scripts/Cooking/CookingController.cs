using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Gameplay.Crafting
{
    public class CookingController
    {
        private readonly CookingConfig _config;
        private readonly CraftController _craftController;
        private readonly Dictionary<FoodRecepie, float> _currentCookingTimes = new();

        public CookingController(CookingConfig config, CraftController craftController)
        {
            _config = config;
            _craftController = craftController;
        }

        public IReadOnlyCollection<CookingGroup> GetGroups() 
            => _config.CookingGroups;

        public float GetCookingTimeLeft(FoodRecepie recepie) 
            => _currentCookingTimes.GetValueOrDefault(recepie, 0);

        public bool CanCook(FoodRecepie recepie) 
            => _craftController.CanCraft(recepie, out int _);

        public bool CanCook(FoodRecepie recepie, out int possibleTimes) =>
            _craftController.CanCraft(recepie, out possibleTimes);

        public async UniTask CookAsync(CookingGroup cookingGroup, int times = 1)
        {
            var recepie = cookingGroup.Recepie;
            var time = cookingGroup.CookTime;

            if (_currentCookingTimes.ContainsKey(recepie))
                throw new ArgumentException("Recepie is already cooking");
            
            for (int i = 0; i < times; i++)
            {
                _currentCookingTimes[recepie] = time;
                
                while (_currentCookingTimes[recepie] > 0)
                {
                    await UniTask.DelayFrame(1);
                    _currentCookingTimes[recepie] = Time.deltaTime;
                }
                
                _craftController.Craft(recepie, 1);
            }

            _currentCookingTimes.Remove(recepie);
        }
    }
}