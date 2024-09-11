using System.Collections;
using DoubleDCore.Tween.Base;
using UnityEngine;

namespace DoubleDCore.Tween.Effects
{
    [RequireComponent(typeof(RectTransform))]
    public class UIShakeEffect : AnimationEffect
    {
        [SerializeField] private bool _playOnAwake;
        [SerializeField] private float _deltaDuration = 0.025f;
        [SerializeField] private float _strength = 0.1f;

        private RectTransform _rectTransform;
        private Vector2 _originalPosition;
        private bool _bringBackAnimation = false;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _originalPosition = _rectTransform.anchoredPosition;

            if (_playOnAwake)
                StartAnimation();
        }

        private void OnEnable()
        {
            if (_bringBackAnimation)
                StartAnimation();
        }

        private void OnDisable()
        {
            _bringBackAnimation = IsActive;
            CancelAnimation();
        }

        protected override void OnStartAnimation()
        {
            _coroutine = StartCoroutine(StartShakeAnimation());
        }

        protected override void OnStopAnimation()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = null;

            _rectTransform.anchoredPosition = _originalPosition;
        }

        protected override void OnCancelAnimation()
        {
            OnStopAnimation();
        }

        private Coroutine _coroutine;

        private IEnumerator StartShakeAnimation()
        {
            while (true)
            {
                var x = Random.Range(-_strength, _strength);
                var y = Random.Range(-_strength, _strength);

                Vector2 randomOffset = new(x, y);

                _rectTransform.anchoredPosition = _originalPosition + randomOffset;
                yield return new WaitForSeconds(_deltaDuration);
            }
        }
    }
}