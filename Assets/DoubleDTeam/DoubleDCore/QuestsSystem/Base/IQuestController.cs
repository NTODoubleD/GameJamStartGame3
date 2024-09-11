using System;

namespace DoubleDCore.QuestsSystem.Base
{
    public interface IQuestController
    {
        public event Action<IQuest> QuestIssued;
        public event Action<IQuest> QuestCompleted;

        public void Register(IQuest quest);
        public void Remove(IQuest quest);

        public void IssueQuest(IQuest quest);
        public void CloseQuest(IQuest quest);
    }
}