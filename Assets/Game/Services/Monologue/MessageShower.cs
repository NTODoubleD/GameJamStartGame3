using System;
using DoubleDCore.Extensions;
using DoubleDCore.Service;
using DoubleDCore.TimeTools;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Monologue
{
    public class MessageShower : MonoService
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private AudioSource _source;

        public event Action Started;
        public event Action Performed;

        public bool IsPrintText { get; private set; }

        private Action _endAction;
        private Timer _timer;

        [Inject]
        private void Init(ITimersFactory timersFactory)
        {
            _timer = timersFactory.Create(TimeBindingType.ScaledTime);
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