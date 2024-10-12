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
        [SerializeField] private TrainingInfo _heatingInfo;
        [SerializeField] private TrainingInfo _cookingInfo;
        [SerializeField] private TrainingInfo _waterInfo;
        [SerializeField] private TrainingInfo _sleepInfo;
        [SerializeField] private TrainingInfo _deerCutInfo;
        [SerializeField] private TrainingInfo _upgradeInfo;
        [SerializeField] private TrainingInfo _strongFrostInfo;
        
        public TrainingInfo InterfaceInfo => _interfaceInfo;
        public TrainingInfo SleighInfo => _sleighInfo;
        public TrainingInfo DeerCareInfo => _deerCareInfo;
        public TrainingInfo HeatingInfo => _heatingInfo;
        public TrainingInfo CookingInfo => _cookingInfo;
        public TrainingInfo WaterInfo => _waterInfo;
        public TrainingInfo SleepInfo => _sleepInfo;
        public TrainingInfo DeerCutInfo => _deerCutInfo;
        public TrainingInfo UpgradeInfo => _upgradeInfo;
        public TrainingInfo StrongFrostInfo => _strongFrostInfo;
    }
}