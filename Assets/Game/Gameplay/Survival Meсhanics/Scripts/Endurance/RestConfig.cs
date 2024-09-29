using UnityEngine;

namespace Game.Gameplay.SurvivalMeсhanics.Endurance
{
    [CreateAssetMenu(fileName = "Rest Config", menuName = "Configs/Rest")]
    public class RestConfig : ScriptableObject
    {
        [SerializeField, Tooltip("Per Second")]
        private float _restoreValue;
        
        public float RestoreValue => _restoreValue;
    }
}