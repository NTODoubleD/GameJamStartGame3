using UnityEngine;

namespace Game.Gameplay.SurvivalMeсhanics.Hunger
{
    [CreateAssetMenu(fileName = "Hunger Config", menuName = "Configs/Hunger")]
    public class HungerConfig : ScriptableObject
    {
        [SerializeField] private int[] _consumptions;

        public int GetConsumption(int level) => _consumptions[level - 1];
    }
}