using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DoubleDCore.Tween.Effects
{
    public class NumberFeedback : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Color _increaseColor;
        [SerializeField] private Color _decreaseColor;
        [SerializeField] private Vector2 _animationOffset;
        [SerializeField] private float _scaleFactor = 1.5f;
        [SerializeField] private float _endScaleFactor = 0.8f;
        [SerializeField] private AnimationCurve _scaleCurve;

        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private bool _isAnimating = false;
        private int _currentDelta;
        private Action _currentCallback;

        public void StartFeedback(int delta, Action onEnd = null)
        {
            _currentDelta += delta;
            SetText(_currentDelta);

            if (_isAnimating)
                return;

            _isAnimating = true;
            _currentCallback = onEnd;

            switch (_currentDelta)
            {
                case > 0:
                    IncreaseSequenceAnimation(OnFeedbackEnd);
                    break;
                case < 0:
                    DecreaseSequenceAnimation(OnFeedbackEnd);
                    break;
                default:
                    OnFeedbackEnd();
                    break;
            }
        }

        private void OnFeedbackEnd()
        {
            _currentDelta = 0;
            _isAnimating = false;
            _currentCallback?.Invoke();
        }

        private async void IncreaseSequenceAnimation(Action onEnd = null)
        {
            _text.rectTransform.DOLocalMove(_animationOffset, 0);
            _text.rectTransform.DOScale(_scaleFactor, 0);

            DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1, 0.5f);

            await UniTask.WaitForSeconds(2f);

            _text.rectTransform.DOLocalMove(Vector3.zero, 1).SetEase(_scaleCurve).OnComplete(() => onEnd?.Invoke());
            _text.rectTransform.DOScale(_endScaleFactor, 1).SetEase(_scaleCurve);
            DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0, 1f);
        }

        private async void DecreaseSequenceAnimation(Action onEnd = null)
        {
            _text.rectTransform.DOLocalMove(Vector3.zero, 0);
            _text.rectTransform.DOScale(_endScaleFactor, 0);

            _text.rectTransform.DOLocalMove(_animationOffset, 1).SetEase(_scaleCurve);
            _text.rectTransform.DOScale(_scaleFactor, 1).SetEase(_scaleCurve);

            DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 1, 1f);

            await UniTask.WaitForSeconds(2f);

            DOTween.To(() => _canvasGroup.alpha, x => _canvasGroup.alpha = x, 0, 1f)
                .OnComplete(() => onEnd?.Invoke());
        }

        private void SetText(int delta)
        {
            _text.text = delta >= 0 ? $"+{delta}" : $"{delta}";
            _text.color = delta >= 0 ? _increaseColor : _decreaseColor;
        }
    }
}