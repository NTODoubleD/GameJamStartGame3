using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Gameplay.Buildings
{
    public class BuildingConstructionAnimator : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _dustEffect;
        [SerializeField] private float _middleDelay;

        public void Animate(UnityAction onMiddleOfConstruction = null)
        {
            _dustEffect.Play();
            StartCoroutine(DelayedCall(onMiddleOfConstruction, _middleDelay));
        }

        private IEnumerator DelayedCall(UnityAction call, float delay)
        {
            yield return new WaitForSeconds(delay);
            call?.Invoke();
        }
    }
}