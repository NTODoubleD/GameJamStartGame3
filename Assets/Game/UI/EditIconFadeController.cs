using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class EditIconFadeController : MonoBehaviour
    {
        [SerializeField] private PointerHoverDetector _pointerHoverDetector;
        [SerializeField] private Image _targetImage;
        [SerializeField] private float _fadeDuration;

        private void OnEnable()
        {
            _pointerHoverDetector.PointEntered.AddListener(OnPointerEntered);
            _pointerHoverDetector.PointExited.AddListener(OnPointerExited);
        }

        private void OnDisable()
        {
            _pointerHoverDetector.PointEntered.RemoveListener(OnPointerEntered);
            _pointerHoverDetector.PointExited.RemoveListener(OnPointerExited);
        }

        private void OnPointerEntered()
        {
            _targetImage.DOKill();
            _targetImage.DOFade(1, _fadeDuration).SetEase(Ease.InOutQuad);
        }

        private void OnPointerExited()
        {
            _targetImage.DOKill();
            _targetImage.DOFade(0, _fadeDuration).SetEase(Ease.InOutQuad);
        }
    }
}