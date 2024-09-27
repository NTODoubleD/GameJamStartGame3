using DoubleDCore.QuestsSystem.Base;
using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;
using UnityEngine;

namespace Game.Quests.Base
{
    public abstract class YakutSubTask : QuestBehaviour
    {
        [SerializeField] private TranslatedText _taskName;

        public string TaskName => _taskName.GetText();

        public abstract override void Play();
        public abstract override void Close();

        public override string GenerateIdentifier()
        {
            return base.GenerateIdentifier() + gameObject.name.ToLower().Replace(" ", "");
        }
    }
}