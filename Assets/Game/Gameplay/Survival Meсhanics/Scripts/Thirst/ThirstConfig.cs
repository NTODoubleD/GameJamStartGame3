using System.Collections.Generic;
using Game.Gameplay.Items;
using UnityEngine;

namespace Game.Gameplay.SurvivalMeсhanics.Thirst
{
    [CreateAssetMenu(menuName = "Configs/Thirst", fileName = "Thirst Config")]
    public class ThirstConfig : ScriptableObject
    {
        [SerializeField] private GameItemInfo[] _waterResources;
        [SerializeField] private float _foodConsumptionMultiplyer = 0.8f;
        [SerializeField] private float _duration = 60;
        
        public float FoodConsumptionMultiplyer => _foodConsumptionMultiplyer;
        public float Duration => _duration;
        public IReadOnlyCollection<GameItemInfo> WaterResources => _waterResources;
    }
}