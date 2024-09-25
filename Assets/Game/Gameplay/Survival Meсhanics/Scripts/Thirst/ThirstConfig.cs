using UnityEngine;

namespace Game.Gameplay.SurvivalMeсhanics.Thirst
{
    [CreateAssetMenu(fileName = "Thirst Config", menuName = "Configs/Thirst")]
    public class ThirstConfig : ScriptableObject
    {
        [SerializeField] private int[] _consumptions;

        public int GetConsumption(int level) => _consumptions[level - 1];
    }
}