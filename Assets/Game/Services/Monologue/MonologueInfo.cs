using System;
using UnityEngine;

namespace Game.Monologue
{
    [Serializable]
    public class MonologueInfo
    {
        [field: SerializeField, TextArea] public string Text { get; private set; }
        [field: SerializeField] public AudioClip VoiceClip { get; private set; }

        [field: SerializeField] public float Duration { get; private set; } = 0;
        [field: SerializeField] public float AfterMessageDuration { get; private set; } = 2;

        public float CharacterInterval => Duration / Text.Length;
    }
}