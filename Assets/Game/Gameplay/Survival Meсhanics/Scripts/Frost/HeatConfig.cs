using UnityEngine;

namespace Game.Gameplay.SurvivalMechanics.Frost
{
    [CreateAssetMenu(fileName = "Heat Config", menuName = "Configs/Heat")]
    public class HeatConfig : ScriptableObject
    {
        [SerializeField, Tooltip("Per Second")]
        private float _heatAddition;

        [SerializeField] private float _heatRestoreValue = 100; 

        public float HeatAddition => _heatAddition;
        public float HeatRestoreValue => _heatRestoreValue;
    }
}