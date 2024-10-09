using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DoubleDCore.UI.Base;
using UnityEngine;

namespace Game.UI
{
    public class UIDot : ButtonListener
    {
        [SerializeField] private Vector2 _fromSize = new(100, 100);
        [SerializeField] private Vector2 _toSize = new(100, 100);
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private AnimationCurve _animationCurve;

        private RectTransform _rectTransform;

        private TweenerCore<Vector2, Vector2, VectorOptions> _selfTween;

        public event Action Clicked;

        protected override void Awake()
        {
            base.Awake();

            _rectTransform = GetComponent<RectTransform>();
        }

        public void SetHighlight(bool isHighlighted)
        {
            _selfTween?.Kill();
            _selfTween = _rectTransform
                .DOSizeDelta(isHighlighted ? _toSize : _fromSize, _duration)
                .SetEase(_animationCurve);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _selfTween?.Kill();
            _rectTransform.sizeDelta = _fromSize;
        }

        protected override void OnButtonClicked()
        {
            Clicked?.Invoke();
        }
    }
}