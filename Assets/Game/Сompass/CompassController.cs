using System.Collections.Generic;
using DoubleDCore.QuestsSystem.Base;
using Game.Quests;
using Game.Quests.Base;
using UnityEngine;
using Zenject;

namespace Game.Сompass
{
    public class CompassController : MonoBehaviour
    {
        [SerializeField] private CommpassView _view;

        private bool _compassActive;

        private bool CompassActive
        {
            get => _compassActive;
            set
            {
                _compassActive = value;
                _view.SetActive(value);
            }
        }

        private IQuestController _questController;

        [Inject]
        private void Init(IQuestController questController)
        {
            _questController = questController;
        }

        public void OnEnable()
        {
            _questController.QuestIssued += OnQuestIssued;
        }

        public void OnDisable()
        {
            _questController.QuestIssued -= OnQuestIssued;
        }

        private void Update()
        {
            if (HasTarget == false)
            {
                if (CompassActive)
                    CompassActive = false;

                return;
            }

            if (CompassActive == false)
                CompassActive = true;

            _view.UpdateCompass(_targets[0].transform.position);
        }

        private bool HasTarget => _targets.Count > 0;

        private readonly List<VisitAreaTask> _targets = new();

        private void OnQuestIssued(IQuest quest)
        {
            if (quest is not YakutQuest yakutQuest)
                return;

            CompassActive = false;

            _targets.Clear();

            foreach (var subTask in yakutQuest.SubTasks)
            {
                if (subTask is not VisitAreaTask visitAreaTask)
                    continue;

                visitAreaTask.QuestCompleted += OnTaskCompleted;
                _targets.Add(visitAreaTask);
            }
        }

        private void OnTaskCompleted(IQuest quest)
        {
            if (quest is not VisitAreaTask visitAreaTask)
                return;

            var target = _targets.Find(t => t.Equals(visitAreaTask));

            if (target is null)
                return;

            target.QuestCompleted -= OnTaskCompleted;
            _targets.Remove(visitAreaTask);
        }
    }
}