using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DoubleDCore.QuestsSystem.Base;
using UnityEngine;
using Zenject;

namespace Game.Quests
{
    public class QuestSequence : MonoBehaviour
    {
        [SerializeField] private List<QuestBehaviour> _sequence;

        private int _questCounter;

        private IQuestController _questController;

        [Inject]
        private void Init(IQuestController questController)
        {
            _questController = questController;
        }

        private void Start()
        {
            foreach (var questBehaviour in _sequence)
                _questController.Register(questBehaviour);

            _questController.IssueQuest(_sequence[_questCounter]);
        }

        private void OnEnable()
        {
            _questController.QuestCompleted += OnQuestCompleted;
        }

        private void OnDisable()
        {
            _questController.QuestCompleted -= OnQuestCompleted;
        }

        private async void OnQuestCompleted(IQuest obj)
        {
            _questCounter++;

            if (_questCounter >= _sequence.Count)
                return;

            await UniTask.WaitForSeconds(0.5f);

            _questController.IssueQuest(_sequence[_questCounter]);
        }
    }
}