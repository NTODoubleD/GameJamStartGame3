using DG.Tweening;
using UnityEngine;

namespace Game.Сompass
{
    public class Compass3D : CommpassView
    {
        [SerializeField] private Transform _root;

        private Vector3 _cachedDirection;
        private Tweener _currentTweener;
        
        public override void UpdateCompass(Vector3 targetPosition)
        {
            _cachedDirection = targetPosition - _root.position;
            _cachedDirection.y = 0;
            _root.forward = _cachedDirection.normalized;
        }

        public override void SetActive(bool active)
        {
            if (_currentTweener != null && _currentTweener.IsActive())
                _currentTweener.Kill();

            if (active)
                _currentTweener = _root.DOScale(1.36f, 0.4f).SetEase(Ease.OutBack);
            else
                _currentTweener = _root.DOScale(0, 0.4f).SetEase(Ease.InBack);
        }
    }
}