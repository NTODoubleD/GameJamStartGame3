using UnityEngine;

namespace Game.Gameplay.SurvivalMechanics.Frost
{
    [CreateAssetMenu(fileName = "Heat Config", menuName = "Configs/Heat", order = 0)]
    public class HeatConfig : ScriptableObject
    {
        [SerializeField, Tooltip("Per Second")]
        private float _heatAddition;

        public float HeatAddition => _heatAddition;
    }
}