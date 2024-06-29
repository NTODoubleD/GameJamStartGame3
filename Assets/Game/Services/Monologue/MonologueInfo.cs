using System;
using UnityEngine;

namespace Game.Monologue
{
    [Serializable]
    public class MonologueInfo
    {
        [field: SerializeField, TextArea] public string Text { get; private set; }
        [field: SerializeField] public AudioClip Clip { get; private set; }

        [field: SerializeField] public float AnimationDelay { get; private set; } = 0;
        [field: SerializeField] public float WaitDuration { get; private set; } = 2;
    }
}