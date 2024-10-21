using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Data
{
    [CreateAssetMenu(menuName = "Configs/TrainingConfigs", fileName = "NewTrainingConfigs")]
    public class TrainingConfigs : ScriptableObject
    {
        [SerializeField] private TrainingInfo[] _trainingInfos;

        public IEnumerable<TrainingInfo> TrainingInfos => _trainingInfos;
    }
}