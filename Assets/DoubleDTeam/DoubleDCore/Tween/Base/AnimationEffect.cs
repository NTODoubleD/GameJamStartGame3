using System;
using UnityEngine;

namespace DoubleDCore.Tween.Base
{
    public abstract class AnimationEffect : MonoBehaviour
    {
        public bool IsActive { get; private set; }

        public event Action Started;
        public event Action Performed;
        public event Action Canceled;

        public void StartAnimation()
        {
            if (IsActive)
                CancelAnimation();

            IsActive = true;
            OnStartAnimation();
            Started?.Invoke();
        }

        public void StopAnimation()
        {
            IsActive = false;
            OnStopAnimation();
            Performed?.Invoke();
        }

        public void CancelAnimation()
        {
            IsActive = false;
            OnCancelAnimation();
            Canceled?.Invoke();
        }

        protected abstract void OnStartAnimation();

        protected abstract void OnStopAnimation();

        protected abstract void OnCancelAnimation();
    }
}