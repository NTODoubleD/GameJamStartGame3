using UnityEngine;

namespace Game.Gameplay.SurvivalMeсhanics.Exhaustion
{
    [CreateAssetMenu(fileName = "Exhaustion Config", menuName = "Configs/Exhaustion")]
    public class ExhaustionConfig : ScriptableObject
    {
        [SerializeField, Tooltip("Per Second")] private int _damage;
        
        public int Damage => _damage;
    }
}