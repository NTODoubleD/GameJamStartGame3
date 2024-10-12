using DoubleDCore.Service;
using Game.Quests.Base;
using UnityEngine;

namespace Game.Quests
{
    public class SceneQuests : MonoService
    {
        [SerializeField] private YakutQuest _travelQuest;
        [SerializeField] private YakutQuest _resourcesQuest;
        [SerializeField] private YakutSubTask _deerCarePastureSubTask;
        
        public YakutQuest TravelQuest => _travelQuest;
        public YakutQuest ResourcesQuest => _resourcesQuest;
        public YakutSubTask DeerCarePastureSubTask => _deerCarePastureSubTask;
    }
}