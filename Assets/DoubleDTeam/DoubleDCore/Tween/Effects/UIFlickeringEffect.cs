using System.Collections;
using DoubleDCore.Tween.Base;
using UnityEngine;
using UnityEngine.UI;

namespace DoubleDCore.Tween.Effects
{
    [RequireComponent(typeof(Image))]
    public class UIFlickeringEffect : AnimationEffect
    {
        [SerializeField] private bool _playOnAwake;
        [SerializeField] private float _duration;
        [SerializeField] private Color _fromColor;
        [SerializeField] private Color _toColor;
        [SerializeField] private AnimationCurve _curve;

        private Image _image;
        private bool _bringBackAnimation = false;

        private void Awake()
        {
            _image = GetComponent<Image>();

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
            _coroutine = StartCoroutine(Animation());
        }

        protected override void OnStopAnimation()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = null;

            _image.color = _fromColor;
        }

        protected override void OnCancelAnimation()
        {
            OnStopAnimation();
        }

        private Coroutine _coroutine;

        private IEnumerator Animation()
        {
            _image.color = _fromColor;

            float remainingTime = _duration;

            while (true)
            {
                yield return null;

                remainingTime -= Time.deltaTime;

                if (remainingTime < 0)
                    remainingTime = _duration;

                float progress = 1 - remainingTime / _duration;
                float time = _curve.Evaluate(progress);

                float r = Mathf.Lerp(_fromColor.r, _toColor.r, time);
                float g = Mathf.Lerp(_fromColor.g, _toColor.g, time);
                float b = Mathf.Lerp(_fromColor.b, _toColor.b, time);
                float a = Mathf.Lerp(_fromColor.a, _toColor.a, time);

                _image.color = new Color(r, g, b, a);
            }
        }
    }
}