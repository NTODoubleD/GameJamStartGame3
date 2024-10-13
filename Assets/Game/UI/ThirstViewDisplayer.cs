using Game.Gameplay.SurvivalMeсhanics.Thirst;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class ThirstViewDisplayer : MonoBehaviour
    {
        [SerializeField] private GameObject _view;

        private ThirstController _thirstController;
        
        [Inject]
        private void Init(ThirstController thirstController)
        {
            _thirstController = thirstController;
            _thirstController.EffectStarted += OnEffectStarted;
            _thirstController.EffectEnded += OnEffectEnded;
        }

        private void OnEffectStarted()
        {
            _view.SetActive(true);
        }

        private void OnEffectEnded()
        {
            _view.SetActive(false);
        }

        private void OnDestroy()
        {
            _thirstController.EffectStarted -= OnEffectStarted;
            _thirstController.EffectEnded -= OnEffectEnded;
        }
    }
}