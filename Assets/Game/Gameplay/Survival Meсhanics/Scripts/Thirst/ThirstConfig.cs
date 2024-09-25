using UnityEngine;

namespace Game.Gameplay.SurvivalMeсhanics.Thirst
{
    [CreateAssetMenu(fileName = "Thirst Config", menuName = "Configs/Thirst")]
    public class ThirstConfig : ScriptableObject
    {
        [SerializeField] private float[] _consumptions;

        public float GetConsumption(int level) => _consumptions[level - 1];
    }
}