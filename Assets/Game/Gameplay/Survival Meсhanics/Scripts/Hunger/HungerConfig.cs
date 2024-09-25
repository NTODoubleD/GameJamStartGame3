using UnityEngine;

namespace Game.Gameplay.SurvivalMeсhanics.Hunger
{
    [CreateAssetMenu(fileName = "Hunger Config", menuName = "Configs/Hunger")]
    public class HungerConfig : ScriptableObject
    {
        [SerializeField] private float[] _consumptions;

        public float GetConsumption(int level) => _consumptions[level - 1];
    }
}