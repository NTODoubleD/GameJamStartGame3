using System;

namespace DoubleDCore.QuestsSystem.Data
{
    [Serializable]
    public class EncryptQuest
    {
        public string ID;
        public int Progress;
        public QuestStatus Status;
    }
}