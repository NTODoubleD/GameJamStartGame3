using System;
using DoubleDCore.TranslationTools;
using UnityEngine;

namespace DoubleDCore.QuestsSystem.Data
{
    [Serializable]
    public class QuestInfo
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private TranslatedText _description;
        [SerializeField, Min(1)] private int _maxProgress = 1;
        [SerializeField] private QuestLayer _questLayer;

        public Sprite Sprite => _sprite;
        public TranslatedText Description => _description;
        public int MaxProgress => _maxProgress;
        public QuestLayer QuestLayer => _questLayer;
    }
}