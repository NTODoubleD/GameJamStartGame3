using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DoubleDCore.TranslationTools;
using DoubleDCore.TranslationTools.Extensions;
using Infrastructure;
using Infrastructure.States;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Lyrics
{
    public class LyricsTextShower : MonoBehaviour
    {
        [SerializeField] private TranslatedText[] _texts;
        [SerializeField] private float _appearanceDuration = 1f;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _textHolder;
        [SerializeField] private CanvasGroup _mouseIcon;

        private bool _isClick;

        private GameInput _gameInput;
        private GameStateMachine _stateMachine;

        [Inject]
        private void Init(GameInput gameInput, GameStateMachine stateMachine)
        {
            _gameInput = gameInput;
            _stateMachine = stateMachine;
        }

        public async void Start()
        {
            foreach (var lyricsText in _texts)
            {
                _textHolder.text = lyricsText.GetText();
                _mouseIcon.alpha = 0;

                StartCoroutine(AppearanceAnimation(_appearanceDuration));

                await UniTask.WaitForSeconds(_appearanceDuration + 0.5f);

                _isClick = false;

                var tween = _mouseIcon.DOFade(1, _appearanceDuration);

                await UniTask.WaitUntil(() => _isClick);

                tween.Kill();
            }

            if (_stateMachine.CurrentState is LyricsState lyricsState == false)
                throw new Exception($"{nameof(LyricsTextShower)} works out of context {nameof(LyricsState)}");

            lyricsState.OnLyricsEnd();
        }

        public void OnEnable()
        {
            _gameInput.UI.Click.performed += OnClick;
            _gameInput.UI.Confirm.performed += OnClick;
        }

        private void OnDisable()
        {
            _gameInput.UI.Click.performed -= OnClick;
            _gameInput.UI.Confirm.performed -= OnClick;
        }

        private void OnClick(InputAction.CallbackContext obj)
        {
            _isClick = true;
        }

        private IEnumerator AppearanceAnimation(float duration)
        {
            float remainingTime = duration;

            _canvasGroup.alpha = 0f;

            while (remainingTime > 0)
            {
                yield return null;

                remainingTime -= Time.deltaTime;

                if (remainingTime <= 0)
                    break;

                float progress = 1 - remainingTime / duration;

                _canvasGroup.alpha = progress;
            }

            _canvasGroup.alpha = 1f;
        }
    }
}