using UnityEngine;

namespace Game.Gameplay.Configs
{
    [CreateAssetMenu(fileName = "Deer Illnesses Config", menuName = "Configs/DeerIllnesses")]
    public class DeerIllnessesConfig : ScriptableObject
    {
        [Header("Cast Ill Settings")] 
        [SerializeField] private float _illnessChance = 0.4f;
        [SerializeField] private int _bigFlockIllnessesCount = 2;
        [SerializeField] private int _smallFlockIllnessesCount = 1;
        [SerializeField] private int _bigFlockCount = 4;

        [Header("Continue Ill Settings")] 
        [SerializeField] private int _easySickDays = 1;
        [SerializeField] private int _deathSickDays = 3;
        
        public float IllnessChance => _illnessChance;
        public int BigFlockIllnessesCount => _bigFlockIllnessesCount;
        public int SmallFlockIllnessesCount => _smallFlockIllnessesCount;
        public int BigFlockCount => _bigFlockCount;
        public int EasySickDays => _easySickDays;
        public int DeathSickDays => _deathSickDays;
    }
}