using DG.Tweening;
using UnityEngine;

namespace Game.Gameplay.Interaction
{
    public class InteractionTriggerView : MonoBehaviour
    {
        [SerializeField] private InteractionTrigger _connectedTrigger;
        [SerializeField] private Transform _animatedCircle;

        [Header("Animation Settings")] 
        [SerializeField] private float _duration;
        [SerializeField] private float _upperScale;

        private void OnEnable()
        {
            _connectedTrigger.Entered += OnPlayerEntered;
            _connectedTrigger.Exited += OnPlayerExited;
        }

        private void OnDisable()
        {
            _connectedTrigger.Entered -= OnPlayerEntered;
            _connectedTrigger.Exited -= OnPlayerExited;
        }

        private void OnPlayerEntered(InteractionTrigger trigger)
        {
            _animatedCircle.DOScale(_upperScale, _duration).SetEase(Ease.OutBack);

        }

        private void OnPlayerExited(InteractionTrigger trigger)
        {
            _animatedCircle.DOScale(1, _duration).SetEase(Ease.InBack);
        }
    }
}