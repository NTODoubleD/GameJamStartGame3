using System;
using System.Collections.Generic;
using System.Linq;
using DoubleDCore.QuestsSystem.Base;
using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;
using UnityEngine;

namespace Game.Quests.Base
{
    public class YakutQuest : QuestBehaviour
    {
        [SerializeField] private TranslatedText _header;
        [SerializeField] private YakutSubTask[] _subTasks;

        public string Header => _header.GetText();

        public IEnumerable<YakutSubTask> SubTasks => _subTasks;

        public event Action<YakutQuest> TaskProgressChanged;

        public override void Play()
        {
            foreach (var subQuest in _subTasks)
            {
                subQuest.ProgressChanged += OnProgressChanged;
                subQuest.Play();
            }
        }

        public override void Close()
        {
            foreach (var subQuest in _subTasks)
            {
                subQuest.ProgressChanged -= OnProgressChanged;
                subQuest.Close();
            }
        }

        private void OnProgressChanged(IQuest quest)
        {
            TaskProgressChanged?.Invoke(this);

            if (SubTasks.All(t => t.Progress >= t.MaxProgress))
                PerformQuest();
        }

        public override string GenerateIdentifier()
        {
            return base.GenerateIdentifier() + gameObject.name.ToLower().Replace(" ", "");
        }
    }
}