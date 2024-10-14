using System;
using System.Collections.Generic;
using DoubleDCore.QuestsSystem.Base;
using DoubleDCore.UI.Base;
using Game.Gameplay.DayCycle;
using Game.Gameplay.SurvivalMechanics.Frost;
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
        private readonly Dictionary<TrainingInfo, Func<bool>> _trainingConditions;
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
        private readonly FrostController _frostController;
        private readonly GameInput _gameInput;

        private bool _isDayStarted = true;
        private bool _isHikeResultPageOpened = false;
        private bool _isRestPageOpened;
        private bool _isCookingPageOpened;
        private bool _isRadialItemsMenuOpened;
        
        public GameTrainingsStarter(GameTrainingsConfig config, GameTrainingController trainingController,
            IQuestController questController, SceneQuests sceneQuests, 
            IUIManager uiManager, DayCycleController dayCycleController,
            FrostController frostController, GameInput gameInput)
        {
            _config = config;
            _trainingController = trainingController;
            _questController = questController;
            _sceneQuests = sceneQuests;
            _uiManager = uiManager;
            _dayCycleController = dayCycleController;
            _frostController = frostController;
            _gameInput = gameInput;

            _questStartTrainings = new Dictionary<IQuest, TrainingInfo>()
            {
                {_sceneQuests.TravelQuest, _config.SleighInfo},
                {_sceneQuests.CookingQuest, _config.CookingInfo},
                {_sceneQuests.WaterQuest, _config.WaterInfo},
                {_sceneQuests.SleepQuest, _config.SleepInfo},
                {_sceneQuests.DeerCuttingQuest, _config.DeerCutInfo},
                {_sceneQuests.FirstUpgradeQuest, _config.UpgradeInfo}
            };
            
            _questTaskCompleteTrainings = new Dictionary<IQuest, TrainingInfo>()
            {
                {_sceneQuests.DeerCarePastureSubTask, _config.DeerCareInfo},
                {_sceneQuests.HeatVisitYurtSubTask, _config.HeatingInfo},
            };

            _trainingConditions = new Dictionary<TrainingInfo, Func<bool>>()
            {
                { _config.UpgradeInfo, () => _isDayStarted && !_isHikeResultPageOpened },
                { _config.CookingInfo, () => !_isRestPageOpened },
                { _config.WaterInfo, () => !_isCookingPageOpened },
                { _config.DeerCutInfo, () => _isDayStarted },
                { _config.SleepInfo, () => !_isRadialItemsMenuOpened },
                { _config.StrongFrostInfo, () => _gameInput.Player.enabled && _isDayStarted }
            };

            _questController.QuestIssued += OnQuestIssued;
            _questController.QuestCompleted += OnQuestCompleted;
            _uiManager.PageOpened += OnPageOpened;
            _uiManager.PageClosed += OnPageClosed;
            _dayCycleController.DayStarted += OnDayStarted;
            _dayCycleController.DayEnded += OnDayEnded;
            _frostController.FrostLevelChanged += OnFrostLevelChanged;
        }

        public void Tick()
        {
            if (_delayedTrainings.Count == 0)
                return;

            if (_trainingConditions.ContainsKey(_delayedTrainings.Peek()))
            {
                if (_trainingConditions[_delayedTrainings.Peek()].Invoke())
                    StartTraining(_delayedTrainings.Dequeue());
            }
            else
            {
                StartTraining(_delayedTrainings.Dequeue());
            }
        }
        
        private void OnFrostLevelChanged(FrostLevel level)
        {
            if (level == FrostLevel.Strong)
                StartTraining(_config.StrongFrostInfo);
        }

        private void OnPageOpened(IPage page)
        {
            if (page is ResourceWatcherPage)
                _isHikeResultPageOpened = true;
            else if (page is RestPage)
                _isRestPageOpened = true;
            else if (page is CookingPage)
                _isCookingPageOpened = true;
            else if (page is RadialItemsMenuPage)
                _isRadialItemsMenuOpened = true;
        }
        
        private void OnPageClosed(IPage page)
        {
            if (page is ResourceWatcherPage)
                _isHikeResultPageOpened = false;
            else if (page is RestPage)
                _isRestPageOpened = false;
            else if (page is CookingPage)
                _isCookingPageOpened = false;
            else if (page is RadialItemsMenuPage)
                _isRadialItemsMenuOpened = false;
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
            
            if (_trainingConditions.ContainsKey(trainingInfo) && !_trainingConditions[trainingInfo].Invoke())
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
            _frostController.FrostLevelChanged -= OnFrostLevelChanged;
        }
    }
}