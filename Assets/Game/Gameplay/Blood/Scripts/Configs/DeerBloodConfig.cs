using UnityEngine;

namespace Game.Gameplay.Blood.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Deer Blood Config", menuName = "Configs/Deer Blood Config")]
    public class DeerBloodConfig : ScriptableObject
    {
        [SerializeField] private BloodAppearAnimation _bloodDecalPrefab;
        
        public BloodAppearAnimation BloodDecalPrefab => _bloodDecalPrefab;
    }
}