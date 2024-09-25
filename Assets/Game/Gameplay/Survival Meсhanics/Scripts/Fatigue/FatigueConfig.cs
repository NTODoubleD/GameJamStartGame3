using UnityEngine;

namespace Game.Gameplay.SurvivalMeсhanics.Fatigue
{
    [CreateAssetMenu(fileName = "Fatigue Config", menuName = "Configs/Fatigue")]
    public class FatigueConfig : ScriptableObject
    {
        [SerializeField] private float _speedMultiplier = 0.9f;
        
        public float SpeedMultiplier => _speedMultiplier;
    }
}