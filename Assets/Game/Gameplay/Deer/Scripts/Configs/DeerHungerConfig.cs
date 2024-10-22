using UnityEngine;

namespace Game.Gameplay.Configs
{
    [CreateAssetMenu(fileName = "Deer Hunger Config", menuName = "Configs/DeerHunger")]
    public class DeerHungerConfig : ScriptableObject
    {
        [SerializeField] private float _hungerStep = 0.2f;
        [SerializeField] private float _minimalHungerDegree = 0.4f;
        
        public float HungerStep => _hungerStep;
        public float MinimalHungerDegree => _minimalHungerDegree;
    }
}