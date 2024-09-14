
using DG.Tweening;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Game.Gameplay.Interaction
{
    public class InteractionTriggerView : MonoBehaviour
    {
        [SerializeField] private InteractionTrigger _connectedTrigger;
        [SerializeField] private List<InteractionTriggerPart> _parts;

        private void OnEnable()
        {
            foreach (var part in _parts)
            {
                part.AnimatedPart.sizeDelta = new Vector2(part.LowerScale, part.LowerScale);
                part.Image.color = part.StartColor;
            }
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
            foreach (var part in _parts)
            {
                part.AnimatedPart.DOSizeDelta(new Vector2(part.UpperScale,part.UpperScale), part.Duration).SetEase(Ease.OutBack);
                part.Image.DOColor(part.EndColor, part.Duration).SetEase(Ease.OutBack);
            }
        }

        private void OnPlayerExited(InteractionTrigger trigger)
        {            
            foreach (var part in _parts)
            {           
                part.AnimatedPart.DOSizeDelta(new Vector2(part.LowerScale,part.LowerScale), part.Duration).SetEase(Ease.InBack);
                part.Image.DOColor(part.StartColor, part.Duration).SetEase(Ease.InBack);
            }
        }
    }

    [Serializable]
    public class InteractionTriggerPart
    {
        public RectTransform AnimatedPart;
        public Image Image;
        public float Duration;
        public float LowerScale;
        public float UpperScale;
        
        public Color StartColor;
        public Color EndColor;
    }
}