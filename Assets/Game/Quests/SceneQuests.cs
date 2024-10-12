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
        [SerializeField] private YakutSubTask _heatVisitYurtSubTask;
        [SerializeField] private YakutQuest _cookingQuest;
        [SerializeField] private YakutQuest _waterQuest;
        
        public YakutQuest TravelQuest => _travelQuest;
        public YakutQuest ResourcesQuest => _resourcesQuest;
        public YakutSubTask DeerCarePastureSubTask => _deerCarePastureSubTask;
        public YakutSubTask HeatVisitYurtSubTask => _heatVisitYurtSubTask;
        public YakutQuest CookingQuest => _cookingQuest;
        public YakutQuest WaterQuest => _waterQuest;
    }
}