using UnityEngine;

namespace Game.Gameplay.Blood.Scripts.Configs
{
    [CreateAssetMenu(fileName = "Deer Blood Config", menuName = "Configs/Deer Blood Config")]
    public class DeerBloodConfig : ScriptableObject
    {
        [SerializeField] private GameObject _bloodDecalPrefab;
        
        public GameObject BloodDecalPrefab => _bloodDecalPrefab;
    }
}