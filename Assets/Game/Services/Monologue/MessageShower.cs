using System;
using DoubleDTeam.Containers.Base;
using DoubleDTeam.Extensions;
using DoubleDTeam.TimeTools;
using TMPro;
using UnityEngine;

namespace Game.Monologue
{
    public class MessageShower : MonoModule
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private AudioSource _source;

        public event Action Started;
        public event Action Performed;

        public bool IsPrintText { get; private set; }

        private Action _endAction;
        private Timer _timer;

        private void Awake()
        {
            _timer = new Timer(this, TimeBindingType.ScaledTime);
        }

        public void ShowText(MonologueGroupInfo monologue, Action endAction = null)
        {
            if (IsPrintText)
                throw new Exception($"{nameof(MessageShower)} didn't finish typing the text");

            IsPrintText = true;

            Started?.Invoke();

            Printing(0, monologue);
        }

        public void StopMessaging()
        {
            EndShowText();
        }

        private void EndShowText()
        {
            IsPrintText = false;

            _source.Stop();
            _text.text = string.Empty;

            _endAction?.Invoke();
            Performed?.Invoke();
        }

        private void Printing(int index, MonologueGroupInfo monologue)
        {
            if (index >= monologue.Monologues.Count)
            {
                EndShowText();
                return;
            }

            var monologueCharacter = monologue.Monologues[index];

            _source.clip = monologueCharacter.VoiceClip;
            _source.Play();

            _text.text = monologueCharacter.Text;
            _text.StartRevealCharactersAnim(monologueCharacter.CharacterInterval,
                () => OnPrintingEnd(index, monologue));
        }

        private void OnPrintingEnd(int currentIndex, MonologueGroupInfo monologue)
        {
            var monologueCharacter = monologue.Monologues[currentIndex];

            _source.Stop();

            _timer.Start(monologueCharacter.AfterMessageDuration, () => Printing(++currentIndex, monologue));
        }
    }
}