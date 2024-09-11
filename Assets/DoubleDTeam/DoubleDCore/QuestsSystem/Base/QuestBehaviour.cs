using System;
using DoubleDCore.Attributes;
using DoubleDCore.QuestsSystem.Data;
using UnityEngine;

namespace DoubleDCore.QuestsSystem.Base
{
    public abstract class QuestBehaviour : MonoBehaviour, IQuest
    {
        [ReadOnlyProperty, SerializeField] private string _id;
        [SerializeField] private QuestInfo _questInfo;

        public string ID => _id;

        public event Action<IQuest> ProgressChanged;
        public event Action<IQuest> QuestCompleted;

        public QuestInfo QuestInfo => _questInfo;

        public int CurrentProgress => Progress;

        public int MaxProgress => _questInfo.MaxProgress;

        private int _currentProgress;

        public int Progress
        {
            get => _currentProgress;
            protected set
            {
                _currentProgress = value;
                ProgressChanged?.Invoke(this);

                if (_currentProgress >= MaxProgress)
                {
                    QuestCompleted?.Invoke(this);
                }
            }
        }

        public QuestStatus Status { get; set; }

        public void RestoreProgress(int progress, QuestStatus status)
        {
            Progress = progress;
            Status = status;
        }

        public abstract void Play();
        public abstract void Close();

        public virtual string GenerateIdentifier()
        {
            return "quest/";
        }

        public void SetIdentifier(string id)
        {
            _id = id;
        }
    }
}