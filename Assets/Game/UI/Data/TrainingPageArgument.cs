using System;
using System.Collections.Generic;
using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;
using UnityEngine;
using UnityEngine.Video;

namespace Game.UI.Data
{
    [CreateAssetMenu(menuName = "Tip/Create TrainingPageArgument", fileName = "TrainingPageArgument", order = 0)]
    public class TrainingPageArgument : ScriptableObject
    {
        [SerializeField] private List<TrainingTip> _trainingTips;

        public IReadOnlyList<TrainingTip> TrainingTips => _trainingTips;
    }

    [Serializable]
    public class TrainingTip
    {
        [SerializeField] private VideoClip _video;
        [SerializeField] private Sprite _image;
        [SerializeField] private TranslatedText _text;

        public VideoClip Video => _video;
        public Sprite Image => _image;
        public string Text => _text.GetText();
    }
}