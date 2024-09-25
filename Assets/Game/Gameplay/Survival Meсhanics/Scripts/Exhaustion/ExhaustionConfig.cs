using UnityEngine;

namespace Game.Gameplay.SurvivalMeсhanics.Exhaustion
{
    [CreateAssetMenu(fileName = "Exhaustion Config", menuName = "Configs/Exhaustion")]
    public class ExhaustionConfig : ScriptableObject
    {
        [SerializeField, Tooltip("Per Second")] private float _damage;
        
        public float Damage => _damage;
    }
}