using System.Collections;
using DoubleDCore.Tween.Base;
using UnityEngine;

namespace DoubleDCore.Tween.Effects
{
    public class ShakeEffect : AnimationEffect
    {
        [SerializeField] private bool _playOnAwake;
        [SerializeField] private float _deltaDuration = 0.025f;
        [SerializeField] private float _strength = 0.1f;
        [SerializeField] private float _translationDuration = 0.5f;

        private Vector3 _originalPosition;
        private bool _bringBackAnimation = false;

        private void Awake()
        {
            _originalPosition = transform.localPosition;

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
            _startShakeCoroutine = StartCoroutine(StartShakeAnimation());
        }

        protected override void OnStopAnimation()
        {
            if (_startShakeCoroutine != null)
                StopCoroutine(_startShakeCoroutine);

            _startShakeCoroutine = null;

            _stopShakeCoroutine = StartCoroutine(StopShakeAnimation());
        }

        protected override void OnCancelAnimation()
        {
            if (_startShakeCoroutine != null)
                StopCoroutine(_startShakeCoroutine);

            _startShakeCoroutine = null;

            if (_stopShakeCoroutine != null)
                StopCoroutine(_stopShakeCoroutine);

            _stopShakeCoroutine = null;

            transform.localPosition = _originalPosition;
        }

        private Coroutine _startShakeCoroutine;

        private IEnumerator StartShakeAnimation()
        {
            float remainingTranslationTime = _translationDuration;

            while (true)
            {
                yield return new WaitForSeconds(_deltaDuration);

                remainingTranslationTime -= _deltaDuration;

                if (remainingTranslationTime < 0)
                    remainingTranslationTime = 0;

                float progress = 1 - remainingTranslationTime / _translationDuration;

                float strength = Mathf.Lerp(0, _strength, progress);

                SetRandomOffset(strength);
            }
        }

        private Coroutine _stopShakeCoroutine;

        private IEnumerator StopShakeAnimation()
        {
            float remainingTranslationTime = _translationDuration;

            while (true)
            {
                yield return new WaitForSeconds(_deltaDuration);

                remainingTranslationTime -= _deltaDuration;

                if (remainingTranslationTime < 0)
                {
                    transform.localPosition = _originalPosition;
                    break;
                }

                float progress = 1 - remainingTranslationTime / _translationDuration;

                float strength = Mathf.Lerp(_strength, 0, progress);

                SetRandomOffset(strength);
            }
        }

        private void SetRandomOffset(float strength)
        {
            var x = Random.Range(-strength, strength);
            var y = Random.Range(-strength, strength);
            var z = Random.Range(-strength, strength);

            Vector3 randomOffset = new(x, y, z);

            transform.localPosition = _originalPosition + randomOffset;
        }
    }
}