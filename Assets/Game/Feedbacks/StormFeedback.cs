using DG.Tweening;
using DoubleDCore.Tween.Base;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.VFX;

namespace Game.Feedbacks
{
    public class StormFeedback : AnimationEffect
    {
        [SerializeField] private VisualEffect _defaultSnow;
        [SerializeField] private VisualEffect _snow;
        [SerializeField] private VisualEffect _fog;
        [SerializeField] private Light _directionalLight;

        [SerializeField] private float _endIntensity = 0.3f;
        [SerializeField] private float _blendTime = 5f;

        protected override void OnStartAnimation()
        {
            _defaultSnow.Stop();

            _snow.Play();
            _fog.Play();

            _directionalLight.DOIntensity(_endIntensity, _blendTime);
        }

        protected override void OnStopAnimation()
        {
            _defaultSnow.Play();

            _snow.Stop();
            _fog.Stop();

            _directionalLight.DOIntensity(1f, _blendTime);
        }

        [Button]
        private void StartFeedback()
        {
            StartAnimation();
        }

        [Button]
        private void StopFeedback()
        {
            StopAnimation();
        }
    }
}