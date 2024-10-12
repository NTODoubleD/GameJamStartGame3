using System.Collections.Generic;
using DoubleDCore.QuestsSystem;
using DoubleDCore.QuestsSystem.Base;
using DoubleDCore.UI.Base;
using Game.Gameplay.DayCycle;
using Game.Gameplay.Sleigh;
using Game.Quests;
using Game.Quests.Base;
using Game.Tips.Configs;
using Game.UI.Data;
using Game.UI.Pages;
using Zenject;

namespace Game.Tips
{
    public class GameTrainingsStarter : ITickable
    {
        private readonly Dictionary<IQuest, TrainingInfo> _questStartTrainings;
        private readonly Dictionary<IQuest, TrainingInfo> _questTaskCompleteTrainings;
        private readonly Dictionary<TrainingInfo, bool> _showedTrainings = new();
        private readonly Queue<TrainingInfo> _delayedTrainings = new();
        
        private readonly GameTrainingsConfig _config;
        private readonly GameTrainingController _trainingController;
        private readonly IQuestController _questController;
        private readonly SceneQuests _sceneQuests;
        private readonly IUIManager _uiManager;
        private readonly DayCycleController _dayCycleController;

        private bool _isDayStarted = true;
        private bool _isHikeResultPageOpened = false;
        
        public GameTrainingsStarter(GameTrainingsConfig config, GameTrainingController trainingController,
            IQuestController questController, SceneQuests sceneQuests, 
            IUIManager uiManager, DayCycleController dayCycleController)
        {
            _config = config;
            _trainingController = trainingController;
            _questController = questController;
            _sceneQuests = sceneQuests;
            _uiManager = uiManager;
            _dayCycleController = dayCycleController;

            _questStartTrainings = new Dictionary<IQuest, TrainingInfo>()
            {
                {_sceneQuests.TravelQuest, _config.SleighInfo},
                {_sceneQuests.ResourcesQuest, _config.InterfaceInfo},
            };
            
            _questTaskCompleteTrainings = new Dictionary<IQuest, TrainingInfo>()
            {
                {_sceneQuests.DeerCarePastureSubTask, _config.DeerCareInfo},
            };

            _questController.QuestIssued += OnQuestIssued;
            _questController.QuestCompleted += OnQuestCompleted;
            _uiManager.PageOpened += OnPageOpened;
            _uiManager.PageClosed += OnPageClosed;
            _dayCycleController.DayStarted += OnDayStarted;
            _dayCycleController.DayEnded += OnDayEnded;
        }
        
        public void Tick()
        {
            if (_delayedTrainings.Count == 0)
                return;

            if (_isDayStarted && !_isHikeResultPageOpened)
                StartTraining(_delayedTrainings.Dequeue());
        }

        private void OnPageOpened(IPage page)
        {
            if (page is ResourceWatcherPage)
                _isHikeResultPageOpened = true;
        }
        
        private void OnPageClosed(IPage page)
        {
            if (page is ResourceWatcherPage)
                _isHikeResultPageOpened = false;
        }

        private void OnDayStarted()
        {
            _isDayStarted = true;
        }
        
        private void OnDayEnded()
        {
            _isDayStarted = false;
        }

        private void OnQuestIssued(IQuest quest)
        {
            if (_questStartTrainings.ContainsKey(quest))
                StartTraining(_questStartTrainings[quest]);;

            if (quest is YakutQuest yakutQuest)
            {
                foreach (var task in yakutQuest.SubTasks)
                    task.QuestCompleted += OnQuestTaskCompleted;
            }
        }
        
        private void OnQuestCompleted(IQuest quest)
        {
            if (quest is YakutQuest yakutQuest)
            {
                foreach (var task in yakutQuest.SubTasks)
                    task.QuestCompleted -= OnQuestTaskCompleted;
            }
        }

        private void OnQuestTaskCompleted(IQuest questTask)
        {
            if (_questTaskCompleteTrainings.ContainsKey(questTask))
                StartTraining(_questTaskCompleteTrainings[questTask]);
        }
        
        private void StartTraining(TrainingInfo trainingInfo)
        {
            if (_showedTrainings.ContainsKey(trainingInfo))
                return;
            
            if (_isDayStarted == false || _isHikeResultPageOpened)
            {
                _delayedTrainings.Enqueue(trainingInfo);
                return;
            }

            _showedTrainings[trainingInfo] = true;
            _trainingController.StartTraining(trainingInfo);
        }
        
        ~GameTrainingsStarter()
        {
            _questController.QuestIssued -= OnQuestIssued;
            _questController.QuestCompleted -= OnQuestCompleted;
            _uiManager.PageOpened -= OnPageOpened;
            _uiManager.PageClosed -= OnPageClosed;
            _dayCycleController.DayStarted -= OnDayStarted;
            _dayCycleController.DayEnded -= OnDayEnded;
        }
    }
}