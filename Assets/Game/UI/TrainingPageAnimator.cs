using System;
using DG.Tweening;
using UnityEngine;

namespace Game.UI
{
    public class TrainingPageAnimator : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _background;
        [SerializeField] private CanvasGroup _widget;

        private const float StandardAnimationDuration = 0.5f;

        private void Awake()
        {
            _background.alpha = 0;
            _widget.alpha = 0;
        }

        public void StartOpenAnimation(bool isAnimate = true)
        {
            if (isAnimate == false)
            {
                _background.alpha = 0;
                _background.blocksRaycasts = false;
                _widget.alpha = 1;

                return;
            }

            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(_widget.DOFade(0f, 0f))
                .Append(_background.DOFade(1f, 2f))
                .Append(_widget.DOFade(1f, StandardAnimationDuration));

            sequence.Play().SetUpdate(true);
        }

        public void StartCloseAnimation(Action onEnd, bool isAnimate = true)
        {
            if (isAnimate == false)
            {
                _background.alpha = 0;
                _background.blocksRaycasts = true;
                _widget.alpha = 0;
                onEnd.Invoke();

                return;
            }

            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(_widget.DOFade(0, StandardAnimationDuration))
                .Append(_background.DOFade(0, StandardAnimationDuration))
                .OnComplete(() => onEnd?.Invoke());

            sequence.Play().SetUpdate(true);
        }
    }
}