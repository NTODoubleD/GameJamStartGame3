using Game.Gameplay.Crafting;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Visual
{
    public class CookingVisual : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        private CookingController _cookingController;
        
        [Inject]
        private void Init(CookingController cookingController)
        {
            _cookingController = cookingController;
        }

        private void Awake()
        {
            _cookingController.CookingStarted += EnableParticles;
            _cookingController.CookingEnded += DisableParticles;
        }

        private void EnableParticles()
        {
            _particleSystem.Play();
        }

        private void DisableParticles()
        {
            _particleSystem.Stop();
        }

        private void OnDestroy()
        {
            _cookingController.CookingStarted -= EnableParticles;
            _cookingController.CookingEnded -= DisableParticles;
        }
    }
}