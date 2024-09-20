using System;
using System.Collections.Generic;
using DoubleDCore.QuestsSystem.Base;
using DoubleDCore.QuestsSystem.Data;

namespace DoubleDCore.QuestsSystem
{
    public class QuestController : IQuestController
    {
        public event Action<IQuest> QuestIssued;
        public event Action<IQuest> QuestCompleted;

        private readonly Dictionary<string, IQuest> _questList = new();
        public IEnumerable<IQuest> QuestList => _questList.Values;

        public void Register(IQuest quest)
        {
            _questList.TryAdd(quest.ID, quest);
        }

        public void Remove(IQuest quest)
        {
            _questList.Remove(quest.ID);
        }

        public void IssueQuest(IQuest quest)
        {
            if (_questList.ContainsKey(quest.ID) == false)
                return;

            if (quest.Status is QuestStatus.Completed or QuestStatus.Failed or QuestStatus.Blocked)
                return;

            InitializeQuest(quest);

            QuestIssued?.Invoke(quest);
        }

        private void InitializeQuest(IQuest quest)
        {
            quest.Status = QuestStatus.InProgress;

            quest.QuestCompleted += OnQuestCompleted;
            quest.Play();
        }

        private void OnQuestCompleted(IQuest quest)
        {
            CloseQuest(quest);

            QuestCompleted?.Invoke(quest);
        }

        public void CloseQuest(IQuest quest)
        {
            if (_questList.ContainsKey(quest.ID) == false)
                return;

            quest.Status = QuestStatus.Completed;

            quest.QuestCompleted -= OnQuestCompleted;
            quest.Close();
        }
    }
}