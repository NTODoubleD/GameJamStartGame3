using Game.UI.Data;
using UnityEngine;

namespace Game.Tips.Configs
{
    [CreateAssetMenu(fileName = "Trainings Config", menuName = "Configs/Trainings")]
    public class GameTrainingsConfig : ScriptableObject
    {
        [SerializeField] private TrainingInfo _interfaceInfo;
        [SerializeField] private TrainingInfo _sleighInfo;
        [SerializeField] private TrainingInfo _deerCareInfo;
        
        public TrainingInfo InterfaceInfo => _interfaceInfo;
        public TrainingInfo SleighInfo => _sleighInfo;
        public TrainingInfo DeerCareInfo => _deerCareInfo;
    }
}