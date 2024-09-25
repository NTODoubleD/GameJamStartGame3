using UnityEngine;

namespace Game.Gameplay.SurvivalMeсhanics.Dehydration
{
    [CreateAssetMenu(fileName = "Dehydration Config", menuName = "Configs/Dehydration")]
    public class DehydrationConfig : ScriptableObject
    {
        [SerializeField, Tooltip("Per Second")] private int _damage;
        
        public int Damage => _damage;
    }
}