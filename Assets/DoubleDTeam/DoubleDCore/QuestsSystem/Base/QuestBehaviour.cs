using System;
using DoubleDCore.Attributes;
using DoubleDCore.QuestsSystem.Data;
using Sirenix.OdinInspector;
using UnityEditor;
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
                    PerformQuest();
            }
        }

        public QuestStatus Status { get; set; }

        public void RestoreProgress(int progress, QuestStatus status)
        {
            Progress = progress;
            Status = status;
        }

        protected void PerformQuest()
        {
            QuestCompleted?.Invoke(this);
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

#if UNITY_EDITOR
        [Button("Change ID")]
        private void UpdateID()
        {
            if (EditorUtility.DisplayDialog("Achtung!!!",
                    "Are you sure you want to generate ID?", "Yes", "No") == false)
                return;

            Undo.RecordObject(this, "Change ID");

            SetIdentifier(GenerateIdentifier());

            EditorUtility.SetDirty(this);
        }
#endif
    }
}