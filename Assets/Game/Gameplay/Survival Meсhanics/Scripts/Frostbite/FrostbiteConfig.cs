using UnityEngine;

namespace Game.Gameplay.SurvivalMeсhanics.Frostbite
{
    [CreateAssetMenu(fileName = "Frostbite Config", menuName = "Configs/Frostbite")]
    public class FrostbiteConfig : ScriptableObject
    {
        [SerializeField, Tooltip("Per Second")] private float _damage;
        
        public float Damage => _damage;
    }
}