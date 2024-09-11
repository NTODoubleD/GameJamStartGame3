using System;
using DoubleDCore.Identification;
using DoubleDCore.QuestsSystem.Data;

namespace DoubleDCore.QuestsSystem.Base
{
    public interface IQuest : IIdentifying
    {
        public event Action<IQuest> ProgressChanged;
        public event Action<IQuest> QuestCompleted;

        public QuestStatus Status { get; set; }

        public int CurrentProgress { get; }

        public QuestInfo QuestInfo { get; }

        public void Play();
        public void Close();
    }
}